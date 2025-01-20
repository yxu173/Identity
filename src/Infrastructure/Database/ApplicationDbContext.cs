using Application.Abstractions.Data;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User,IdentityRole<Guid>,Guid>(options), IApplicationDbContext
{
//    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Entity<IdentityUserLogin<Guid>>(entity => entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }));

        builder.Entity<IdentityUserRole<Guid>>(entity => entity.HasKey(e => new { e.UserId, e.RoleId }));

        builder.Entity<IdentityUserToken<Guid>>(entity => entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }));
    }
    
    

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}
