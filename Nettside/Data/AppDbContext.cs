using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nettside.Models;

namespace Nettside.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<GeoChanges> GeoChange { get; set; }
        public DbSet<AreaChange> AreaChanges { get; set; }



        // <summary>
        /// configures the model properties and seeds the database with initial data
        /// </summary>
        /// <param name="NodelBuilder">the builder used to construct the model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // section for identity tables
            base.OnModelCreating(modelBuilder);

            // role IDs for seeding roles
            var sysAdminRoleId = "1";
            var caseWorkerRoleId = "2";
            var privateUserRoleId = "3";

            // Seed roles (Systemadmin, Caseworker, PrivateUser) 

            var roles = new List<IdentityRole>
            {
                 new IdentityRole
                {
                    Name =  "System Administrator",
                    NormalizedName = "SYSADMIN",
                    Id = sysAdminRoleId,
                    ConcurrencyStamp = sysAdminRoleId
                },

                new IdentityRole
                {
                    Name =  "Case worker",
                    NormalizedName = "CASEWORKER",
                    Id = caseWorkerRoleId,
                    ConcurrencyStamp = caseWorkerRoleId
                },

                new IdentityRole
                {
                    Name = "Private User",
                    NormalizedName = "PRIVATEUSER",
                    Id = privateUserRoleId,
                    ConcurrencyStamp = privateUserRoleId
                }

            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // seed SYSADMIN
            var sysAdminId = "1";
            var sysAdminUser = new Users
            {
                Id = sysAdminId,
                UserName = "sysadmin@kartverket.no",
                Email = "sysadmin@kartverket.no",
                NormalizedEmail = "sysadmin@kartverket.no".ToUpper(),
                NormalizedUserName = "sysadmin@kartverket.no".ToUpper(),
                FirstName = "System",
                LastName = "Administrator"
            };

            sysAdminUser.PasswordHash = new PasswordHasher<Users>()
                 .HashPassword(sysAdminUser, "sysAdmin@123");

            modelBuilder.Entity<Users>().HasData(sysAdminUser);


            // adding all roles to sysadmin

            var sysAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                   RoleId = sysAdminRoleId,
                   UserId = sysAdminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = caseWorkerRoleId,
                    UserId = sysAdminId
                },

                 new IdentityUserRole<string>
                {
                    RoleId = privateUserRoleId,
                    UserId = sysAdminId
                }
            };



            modelBuilder.Entity<IdentityUserRole<string>>().HasData(sysAdminRoles);

            var caseWorkerId = "2";
            var caseWorker = new Users
            {
                Id = caseWorkerId,
                UserName = "caseworker@test.com",
                NormalizedUserName = "CASEWORKER@TEST.COM",
                Email = "caseWorker@test.com",
                NormalizedEmail = "CASEWORKER@TEST.COM",
                FirstName = "Test",
                LastName = "Caseworker"
            };

            caseWorker.PasswordHash = new PasswordHasher<Users>()
                .HashPassword(caseWorker, "caseworker@123");

            modelBuilder.Entity<Users>().HasData(caseWorker);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = caseWorkerRoleId,
                    UserId = caseWorkerId
                }

        );


            // seed PRIVATEUSER

            var privateUserId = "3";
            var privateUser = new Users
            {
                Id = privateUserId,
                UserName = "privateUser@test.com",
                NormalizedUserName = "PRIVATEUSER@TEST.COM",
                Email = "privateUser@test.com",
                NormalizedEmail = "PRIVATEUSER@TEST.COM",
                FirstName = "Test",
                LastName = "privateUser"
            };

            privateUser.PasswordHash = new PasswordHasher<Users>()
                .HashPassword(privateUser, "privateUser@123");

            modelBuilder.Entity<Users>().HasData(privateUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = privateUserRoleId,
                    UserId = privateUserId



                });


    }
    }
}

    


