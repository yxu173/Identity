using Application.Users.Logout;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;
public class Logout : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/logout", async (ISender sender, CancellationToken cancellationToken) =>
        {
            Result<bool> result = await sender.Send(new LogoutUserCommand(), cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.Users);

    }
}
