using SharedKernel;

namespace Domain.Roles;

public static class RoleErrors
{
    public static readonly Error RoleCreateError = Error.NotFound(
        "Roles.RoleCreateError",
        "An error occurred while creating the role");
}
