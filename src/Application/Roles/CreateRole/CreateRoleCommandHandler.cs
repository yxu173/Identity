using Application.Abstractions.Messaging;
using Domain.Roles;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Roles.CreateRole;

public class CreateRoleCommandHandler(RoleManager<Role> roleManager)
    : ICommandHandler<CreateRoleCommand, bool>
{
    private readonly RoleManager<Role> _roleManager = roleManager;

    public async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role { Name = request.RoleName };
        IdentityResult result = await _roleManager.CreateAsync(role);
        return result.Succeeded ? Result.Success(true) : Result.Failure<bool>(RoleErrors.RoleCreateError);
    }
}

