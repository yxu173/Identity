using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;
namespace Application.Users.Logout;

public sealed record LogoutUserCommandHandler(SignInManager<User> signInManager) : ICommandHandler<LogoutUserCommand, bool>
{
    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
       await signInManager.SignOutAsync();
       return Result.Success(true);
    }
}
