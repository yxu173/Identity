using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Roles;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IApplicationDbContext context,
    UserManager<User> userManager)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            UserName = command.userName,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        IdentityResult result = await userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            Error[] errors = result.Errors
                .Select(e => new Error(e.Code, e.Description, ErrorType.Validation))
                .ToArray();
            return Result.Failure<Guid>(errors[0]);
        }

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
