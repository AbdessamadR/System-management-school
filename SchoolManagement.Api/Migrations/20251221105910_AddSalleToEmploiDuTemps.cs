using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSalleToEmploiDuTemps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Jour",
                table: "EmploisDuTemps",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Salle",
                table: "EmploisDuTemps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 11, 59, 8, 639, DateTimeKind.Local).AddTicks(4931), "$2a$11$ygs6I1JD5m/dQFdyhEP86ud/nZaxyfP3sEPOMkMW.sfEhNdyygngu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salle",
                table: "EmploisDuTemps");

            migrationBuilder.AlterColumn<string>(
                name: "Jour",
                table: "EmploisDuTemps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 0, 41, 39, 631, DateTimeKind.Local).AddTicks(1614), "$2a$11$6l8fhlETVfyGy7AsOpmXGu8COPVb0FznuXfm64hirU6p4F8uAR3zy" });
        }
    }
}
