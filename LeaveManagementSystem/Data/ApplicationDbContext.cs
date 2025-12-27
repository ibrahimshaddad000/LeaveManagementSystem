using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(
    new IdentityRole
    {
        Id = "136f647a-306c-489e-8c28-93aaf7da5ca7",
        Name = "Employee",
        NormalizedName = "EMPLOYEE",
        ConcurrencyStamp = "ROLE_EMPLOYEE"
    },
    new IdentityRole
    {
        Id = "c437f9d8-94ad-4459-9810-92c648f6da13",
        Name = "Supervisor",
        NormalizedName = "SUPERVISOR",
        ConcurrencyStamp = "ROLE_SUPERVISOR"
    },
    new IdentityRole
    {
        Id = "980d0d0f-c3b4-4bc5-9d4c-8bcee5c4207b",
        Name = "Administrator",
        NormalizedName = "ADMINISTRATOR",
        ConcurrencyStamp = "ROLE_ADMIN"
    }
);


            builder.Entity<ApplicationUser>().HasData(
    new ApplicationUser
    {
        Id = "5d09aab2-4b63-4468-b574-3358e5d605fe",
        Email = "admin@localhost.com",
        NormalizedEmail = "ADMIN@LOCALHOST.COM",
        NormalizedUserName = "ADMIN@LOCALHOST.COM",
        UserName = "admin",
        PasswordHash = "AQAAAAIAAYagAAAAEAy1UyZgoTOHwsnj00XVutLeFYo8F9/Vv9pOPu1a6SnfjGyMQs8sraRXYH95mw6fPA==", // Password: P@ssword1
        EmailConfirmed = true,
        FirstName = "Default",
        LastName = "Admin",
        DateOfBirth = new DateOnly(1990, 1, 1),
        SecurityStamp = "ADMIN_SECURITY_STAMP",
        ConcurrencyStamp = "ADMIN_CONCURRENCY_STAMP"
    }
);


            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "5d09aab2-4b63-4468-b574-3358e5d605fe",
                    RoleId = "980d0d0f-c3b4-4bc5-9d4c-8bcee5c4207b"
                }
            );
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
