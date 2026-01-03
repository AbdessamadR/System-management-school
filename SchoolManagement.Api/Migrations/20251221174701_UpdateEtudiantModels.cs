using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEtudiantModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClasseId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtudiantId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatiereId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 18, 46, 59, 699, DateTimeKind.Local).AddTicks(8770), "$2a$11$qXW47XWSLlkzhCv52RPS4.kRGZBybdRcQVUsh2wzIv4lP/VDt0xyi" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ClasseId",
                table: "Notifications",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EtudiantId",
                table: "Notifications",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_MatiereId",
                table: "Absences",
                column: "MatiereId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences",
                column: "MatiereId",
                principalTable: "Matieres",
                principalColumn: "IdMatiere",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Classes_ClasseId",
                table: "Notifications",
                column: "ClasseId",
                principalTable: "Classes",
                principalColumn: "IdClasse");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Etudiants_EtudiantId",
                table: "Notifications",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "IdEtudiant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Matieres_MatiereId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Classes_ClasseId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Etudiants_EtudiantId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ClasseId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_EtudiantId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Absences_MatiereId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "ClasseId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "EtudiantId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "MatiereId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Notifications",
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
                values: new object[] { new DateTime(2025, 12, 21, 11, 59, 8, 639, DateTimeKind.Local).AddTicks(4931), "$2a$11$ygs6I1JD5m/dQFdyhEP86ud/nZaxyfP3sEPOMkMW.sfEhNdyygngu" });
        }
    }
}
