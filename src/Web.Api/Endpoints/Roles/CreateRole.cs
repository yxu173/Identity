using Application.Roles.CreateRole;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Roles;

public sealed class CreateRole : IEndpoint
{
    public sealed record CreateRoleRequest(string RoleName);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("roles/create", async (CreateRoleRequest request,ISender sender, CancellationToken cancellationToken) =>
        {
            Result<bool> result = await sender.Send(new CreateRoleCommand(request.RoleName), cancellationToken);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Roles);
    }
}
