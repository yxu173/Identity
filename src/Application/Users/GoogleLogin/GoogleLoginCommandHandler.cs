using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Users;
using SharedKernel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Application.Users.GoogleLogin;

public class GoogleLoginCommandHandler(ITokenProvider tokenProvider) : ICommandHandler<GoogleLoginCommand, string>
{
    public Task<Result<string>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = request.Id,
            Email = request.Email,
            UserName = request.UserName,
        };

        // Generate a JWT token
        string token = tokenProvider.Create(user);

        return Task.FromResult(Result.Success(token));
    }
}
