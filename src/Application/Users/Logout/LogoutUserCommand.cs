using Application.Abstractions.Messaging;


namespace Application.Users.Logout;

public sealed record LogoutUserCommand() : ICommand<bool>;
