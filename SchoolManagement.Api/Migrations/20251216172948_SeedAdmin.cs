using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "IdAccount", "Actif", "AdministrateurId", "DateCreation", "EtudiantId", "ParentId", "PasswordHash", "ProfesseurId", "Role", "Username" },
                values: new object[] { 1, true, null, new DateTime(2025, 12, 16, 18, 29, 45, 89, DateTimeKind.Local).AddTicks(7258), null, null, "$2a$11$0BXnUI8P5BTdD23ZWJlDY.Wr9YRh0ZDLKWaqVitTB8oDdmru7ByS2", null, "Admin", "admin@school.com" });

            migrationBuilder.InsertData(
                table: "Administrateurs",
                columns: new[] { "IdAdmin", "AccountId", "Email", "Nom", "Role" },
                values: new object[] { 1, 1, "admin@school.com", "Super", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrateurs",
                keyColumn: "IdAdmin",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1);
        }
    }
}
