using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SharedKernel;

namespace Web.Api.Endpoints.Users;

public class GithubResponse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/GithubResponse", async (HttpContext context) =>
            {
                AuthenticateResult authenticateResult = await context.AuthenticateAsync("GitHub");
                IEnumerable<Claim> claims = authenticateResult.Principal?.Claims;
                return Result.Success(new
                {
                    access_token = authenticateResult.Properties?.GetTokenValue("access_token"),
                    name = claims?.FirstOrDefault(c => c.Type == "name")?.Value,
                    email = claims?.FirstOrDefault(c => c.Type == "email")?.Value
                });
            })
            .WithTags("Users");
    }
}
