using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPrenomToParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurIdProf",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "ProfesseurIdProf",
                table: "Classes",
                newName: "ProfesseurId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_ProfesseurIdProf",
                table: "Classes",
                newName: "IX_Classes_ProfesseurId");

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 20, 22, 18, 0, 234, DateTimeKind.Local).AddTicks(3607), "$2a$11$YCX7EElv/NDbP2ZqGNkBMuxxY2HW3uZq5jZxYFEvO4lA1OvkPpozy" });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurId",
                table: "Classes",
                column: "ProfesseurId",
                principalTable: "Professeurs",
                principalColumn: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Parents");

            migrationBuilder.RenameColumn(
                name: "ProfesseurId",
                table: "Classes",
                newName: "ProfesseurIdProf");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_ProfesseurId",
                table: "Classes",
                newName: "IX_Classes_ProfesseurIdProf");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 19, 1, 252, DateTimeKind.Local).AddTicks(5398), "$2a$11$SNB.1mOyHcDqPwMaNpgfDODYLt8o5fjQe2kaPJBSGjfA.6u4VWsUa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurIdProf",
                table: "Classes",
                column: "ProfesseurIdProf",
                principalTable: "Professeurs",
                principalColumn: "IdProf");
        }
    }
}
