
using Application.Users.ForgetPassword;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class ForgetPassword : IEndpoint
{
    public sealed record ForgetPasswordRequest(string Email);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/forget-password", async (ForgetPasswordRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ForgetPasswordCommand(request.Email);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        }).WithTags(Tags.Users);
    }
}
