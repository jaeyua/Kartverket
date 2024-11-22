using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nettside.Migrations
{
    /// <inheritdoc />
    public partial class InitialTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "1", "System Administrator", "SYSADMIN" },
                    { "2", "2", "Caseworker", "CASEWORKER" },
                    { "3", "3", "PrivateUser", "PRIVATEUSER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "ac426282-4f1f-44bf-bd51-9df3b6874b3a", "sysadmin@kartverket.no", false, "System", "Administrator", false, null, "SYSADMIN@KARTVERKET.NO", "SYSADMIN@KARTVERKET.NO", "AQAAAAIAAYagAAAAEBPMhkgzayA96zAd/KDiwW2rMFcUBoRnNdufw+UsrcXsxgT0pR7UyS6RB8Eq8ZbfeA==", null, false, "34c16870-9e10-400a-bc61-cfcc8fea414b", false, "sysadmin@kartverket.no" },
                    { "2", 0, "57998c9f-2c79-4eff-888c-7f25e6776d98", "caseWorker@test.com", false, "Test", "Caseworker", false, null, "CASEWORKER@TEST.COM", "CASEWORKER@TEST.COM", "AQAAAAIAAYagAAAAEEUC+n84Xh+gdgv2AnB8h1brS1/06uejzNVoA24+O+d/Jx36B4kQnk+m7repe65xMA==", null, false, "a4148575-9f91-46f0-8190-bb3e6c15487d", false, "caseworker@test.com" },
                    { "3", 0, "da4035f9-7eee-4289-bfd0-5a3021949cde", "privateUser@test.com", false, "Test", "privateUser", false, null, "PRIVATEUSER@TEST.COM", "PRIVATEUSER@TEST.COM", "AQAAAAIAAYagAAAAEPU1ZYjiKrCAE4XlVW89R6UWZTsUQqxAsf7pydMHrzNCnvrmqjGK+zwVevJsNfKINQ==", null, false, "2b473978-5f0a-48cd-83c1-67a70b86ac4d", false, "privateUser@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "1" },
                    { "3", "1" },
                    { "2", "2" },
                    { "3", "3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");
        }
    }
}
