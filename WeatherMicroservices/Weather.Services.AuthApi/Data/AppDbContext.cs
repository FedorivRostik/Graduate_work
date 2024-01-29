using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Weather.Services.AuthApi.Models;
using Weather.Services.AuthApi.Utilities.Constants;

namespace Weather.Services.AuthApi.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Id= "2c5e174e-3b0e-446f-86af-483d56fd7210",Name = AppRoles.AdminRole, NormalizedName = AppRoles.AdminRole.ToUpper() }
        );
        var hasher = new PasswordHasher<IdentityUser>();


        //Seeding the User to AspNetUsers table
        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "MYUSER",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );


        //Seeding the relation between our user and role to AspNetUserRoles table
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

    }
}
