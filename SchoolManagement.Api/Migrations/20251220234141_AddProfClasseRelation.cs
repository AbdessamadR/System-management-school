using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProfClasseRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 0, 41, 39, 631, DateTimeKind.Local).AddTicks(1614), "$2a$11$6l8fhlETVfyGy7AsOpmXGu8COPVb0FznuXfm64hirU6p4F8uAR3zy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 20, 23, 54, 37, 819, DateTimeKind.Local).AddTicks(2418), "$2a$11$hVgO31SDMPG6dpg8cttOSe4TghhKeMmqnOTeFkNFxuThyw/esyGEi" });
        }
    }
}
