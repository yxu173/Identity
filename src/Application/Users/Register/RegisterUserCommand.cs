using Application.Abstractions.Messaging;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(string userName , string Email, string FirstName, string LastName, string Password)
    : ICommand<Guid>;
