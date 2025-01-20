using System.Security.Claims;
using Application.Users.GoogleLogin;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

public sealed class GoogleResponse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/GoogleResponse", async (HttpContext context, ISender sender, CancellationToken cancellationToken) =>
            {
                AuthenticateResult result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

                if (!result.Succeeded)
                {
                    return CustomResults.Problem(new Result(false, UserErrors.GoogleLoginFailed));
                }

                IEnumerable<Claim> claims = result.Principal.Claims;
                string? email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                string? name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                string? id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


                var command = new GoogleLoginCommand
                (
                    Id: Guid.Parse(id!),
                    Email: email,
                    UserName: name
                );

                Result<string> tokenResult = await sender.Send(command, cancellationToken);

                return tokenResult.Match(
                    token => Results.Ok(new { Token = token }),
                    CustomResults.Problem);
            })
            .WithTags("Users");
    }
}
