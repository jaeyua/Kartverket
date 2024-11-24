using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nettside.Models;

namespace Nettside.Data
{
    public class AppDbContext : IdentityDbContext<Users, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GeoChanges> GeoChange { get; set; }
        public DbSet<AreaChange> AreaChanges { get; set; }

        /// <summary>
        /// Configures the model properties and seeds the database with initial data.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Section for identity tables
            base.OnModelCreating(modelBuilder);

            // Role IDs for seeding roles
            var caseWorkerRoleId = "1";
            var privateUserRoleId = "2";

            // Seed roles (Caseworker, PrivateUser)
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Caseworker",
                    NormalizedName = "CASEWORKER",
                    Id = caseWorkerRoleId,
                    ConcurrencyStamp = caseWorkerRoleId
                },
                new IdentityRole
                {
                    Name = "PrivateUser",
                    NormalizedName = "PRIVATEUSER",
                    Id = privateUserRoleId,
                    ConcurrencyStamp = privateUserRoleId
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Seed Caseworker user
            var caseWorkerId = "1";
            var caseWorker = new Users
            {
                Id = caseWorkerId,
                UserName = "caseworker@test.com",
                NormalizedUserName = "CASEWORKER@TEST.COM",
                Email = "caseworker@test.com",
                NormalizedEmail = "CASEWORKER@TEST.COM",
                FirstName = "Test",
                LastName = "Caseworker"
            };

            caseWorker.PasswordHash = new PasswordHasher<Users>().HashPassword(caseWorker, "caseworker@123");

            modelBuilder.Entity<Users>().HasData(caseWorker);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = caseWorkerRoleId,
                    UserId = caseWorkerId
                }
            );

            // Seed PrivateUser
            var privateUserId = "2";
            var privateUser = new Users
            {
                Id = privateUserId,
                UserName = "privateUser@test.com",
                NormalizedUserName = "PRIVATEUSER@TEST.COM",
                Email = "privateuser@test.com",
                NormalizedEmail = "PRIVATEUSER@TEST.COM",
                FirstName = "Test",
                LastName = "PrivateUser"
            };

            privateUser.PasswordHash = new PasswordHasher<Users>().HashPassword(privateUser, "privateUser@123");

            modelBuilder.Entity<Users>().HasData(privateUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = privateUserRoleId,
                    UserId = privateUserId
                }
            );
        }
    }
}
