using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProfesseurClassesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfesseurIdProf",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 19, 1, 252, DateTimeKind.Local).AddTicks(5398), "$2a$11$SNB.1mOyHcDqPwMaNpgfDODYLt8o5fjQe2kaPJBSGjfA.6u4VWsUa" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProfesseurIdProf",
                table: "Classes",
                column: "ProfesseurIdProf");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurIdProf",
                table: "Classes",
                column: "ProfesseurIdProf",
                principalTable: "Professeurs",
                principalColumn: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurIdProf",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ProfesseurIdProf",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ProfesseurIdProf",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 16, 18, 29, 45, 89, DateTimeKind.Local).AddTicks(7258), "$2a$11$0BXnUI8P5BTdD23ZWJlDY.Wr9YRh0ZDLKWaqVitTB8oDdmru7ByS2" });
        }
    }
}
