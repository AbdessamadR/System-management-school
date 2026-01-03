using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProfesseurClasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ProfesseurId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ProfesseurId",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "ProfesseurClasse",
                columns: table => new
                {
                    ClassesIdClasse = table.Column<int>(type: "int", nullable: false),
                    ProfesseursIdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesseurClasse", x => new { x.ClassesIdClasse, x.ProfesseursIdProf });
                    table.ForeignKey(
                        name: "FK_ProfesseurClasse_Classes_ClassesIdClasse",
                        column: x => x.ClassesIdClasse,
                        principalTable: "Classes",
                        principalColumn: "IdClasse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesseurClasse_Professeurs_ProfesseursIdProf",
                        column: x => x.ProfesseursIdProf,
                        principalTable: "Professeurs",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 20, 23, 1, 33, 121, DateTimeKind.Local).AddTicks(1959), "$2a$11$pqh15lSUaSpxekiGokNijuplZ6aGQMRuKohJ/wz80MYsq4ytTdKdS" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesseurClasse_ProfesseursIdProf",
                table: "ProfesseurClasse",
                column: "ProfesseursIdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesseurClasse");

            migrationBuilder.AddColumn<int>(
                name: "ProfesseurId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 20, 22, 18, 0, 234, DateTimeKind.Local).AddTicks(3607), "$2a$11$YCX7EElv/NDbP2ZqGNkBMuxxY2HW3uZq5jZxYFEvO4lA1OvkPpozy" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProfesseurId",
                table: "Classes",
                column: "ProfesseurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Professeurs_ProfesseurId",
                table: "Classes",
                column: "ProfesseurId",
                principalTable: "Professeurs",
                principalColumn: "IdProf");
        }
    }
}
