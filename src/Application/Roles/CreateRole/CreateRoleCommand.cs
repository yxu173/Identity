using Application.Abstractions.Messaging;

namespace Application.Roles.CreateRole;

public sealed record CreateRoleCommand(string RoleName) : ICommand<bool>;
