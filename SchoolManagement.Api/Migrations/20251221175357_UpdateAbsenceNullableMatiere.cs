using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAbsenceNullableMatiere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 18, 53, 56, 566, DateTimeKind.Local).AddTicks(1657), "$2a$11$lAE1z5LZ4KleOoMnxg2vb.0BpxTdJzIjvr8pYe3DLIzU1EprYfcqG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAccount",
                keyValue: 1,
                columns: new[] { "DateCreation", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 21, 18, 49, 29, 575, DateTimeKind.Local).AddTicks(7993), "$2a$11$kYmPerqFiOb/hBtTL1CUjOdKmpvvk/Qjt35kiDk6PeoiFFtwdTgTG" });
        }
    }
}
