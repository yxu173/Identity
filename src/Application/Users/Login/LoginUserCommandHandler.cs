using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    SignInManager<User> signInManager,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        SignInResult? result = await signInManager.PasswordSignInAsync
            (user, command.Password, false,true);

        if (!result.Succeeded)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        if (result.IsLockedOut)
        {
            return Result.Failure<string>(UserErrors.LockedOut);
        }

        string token = tokenProvider.Create(user);

        return Result.Success(token);
    }
}

