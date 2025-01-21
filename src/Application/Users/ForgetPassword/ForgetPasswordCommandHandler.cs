using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;
using System.Net;

namespace Application.Users.ForgetPassword;

public sealed class ForgetPasswordCommandHandler(UserManager<User> userManager, IEmailSender emailSender)
    : ICommandHandler<ForgetPasswordCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return (Result<Guid>)Result.Failure(UserErrors.NotFoundByEmail);
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);

        string resetLink = $"https://yourapp.com/reset-password?token={WebUtility.UrlEncode(token)}&email={WebUtility.UrlEncode(request.Email)}";

        await emailSender.SendEmailAsync(request.Email, "Reset Your Password", $"Please reset your password by clicking here: {resetLink}");
        return Result<Guid>.Success(user.Id);
    }
}
