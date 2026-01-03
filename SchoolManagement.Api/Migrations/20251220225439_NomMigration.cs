using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class NomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matieres_Professeurs_ProfesseurId",
                table: "Matieres");

            migrationBuilder.DropIndex(
                name: "IX_Matieres_ProfesseurId",
                table: "Matieres");

            migrationBuilder.DropColumn(
                name: "ProfesseurId",
                table: "Matieres");

            migrationBuilder.CreateTable(
                name: "ProfesseurMatiere",
                columns: table => new
                {
                    MatieresIdMatiere = table.Column<int>(type: "int", nullable: false),
                    ProfesseursIdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesseurMatiere", x => new { x.MatieresIdMatiere, x.ProfesseursIdProf });
                    table.ForeignKey(
                        name: "FK_ProfesseurMatiere_Matieres_MatieresIdMatiere",
                        column: x => x.MatieresIdMatiere,
                        principalTable: "Matieres",
                        principalColumn: "IdMatiere",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesseurMatiere_Professeurs_ProfesseursIdProf",
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
                values: new object[] { new DateTime(2025, 12, 20, 23, 54, 37, 819, DateTimeKind.Local).AddTicks(2418), "$2a$11$hVgO31SDMPG6dpg8cttOSe4TghhKeMmqnOTeFkNFxuThyw/esyGEi" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesseurMatiere_ProfesseursIdProf",
                table: "ProfesseurMatiere",
                column: "ProfesseursIdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesseurMatiere");

            migrationBuilder.AddColumn<int>(
                name: "ProfesseurId",
                table: "Matieres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 20, 23, 1, 33, 121, DateTimeKind.Local).AddTicks(1959), "$2a$11$pqh15lSUaSpxekiGokNijuplZ6aGQMRuKohJ/wz80MYsq4ytTdKdS" });

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_ProfesseurId",
                table: "Matieres",
                column: "ProfesseurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matieres_Professeurs_ProfesseurId",
                table: "Matieres",
                column: "ProfesseurId",
                principalTable: "Professeurs",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
