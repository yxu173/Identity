using Application.Abstractions.Messaging;
using System;

namespace Application.Users.ForgetPassword;

public sealed record ForgetPasswordCommand(string Email) : ICommand<Guid>;
