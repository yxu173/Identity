using Application.Abstractions.Messaging;

namespace Application.Users.GoogleLogin;

public record GoogleLoginCommand( Guid Id,
    string? Email,
    string? UserName) : ICommand<string>;
