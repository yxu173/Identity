using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using SharedKernel;

namespace Web.Api.Endpoints.Users;

public sealed class GoogleLogin : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/GoogleLogin", async (HttpContext context) =>
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = "https://localhost:5001/users/GoogleResponse"
                };
                await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
                return Result.Success();
            })
            .WithTags("Users");
    }
}
