using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEtudiantModels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "MatiereId",
                table: "Absences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 18, 49, 29, 575, DateTimeKind.Local).AddTicks(7993), "$2a$11$kYmPerqFiOb/hBtTL1CUjOdKmpvvk/Qjt35kiDk6PeoiFFtwdTgTG" });

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences",
                column: "MatiereId",
                principalTable: "Matieres",
                principalColumn: "IdMatiere");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "MatiereId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 18, 46, 59, 699, DateTimeKind.Local).AddTicks(8770), "$2a$11$qXW47XWSLlkzhCv52RPS4.kRGZBybdRcQVUsh2wzIv4lP/VDt0xyi" });

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences",
                column: "MatiereId",
                principalTable: "Matieres",
                principalColumn: "IdMatiere",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
