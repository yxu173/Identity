using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public sealed class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

