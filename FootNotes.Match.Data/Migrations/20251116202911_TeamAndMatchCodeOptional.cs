using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FootNotes.MatchManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeamAndMatchCodeOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8e59-af70-731a-b307-c72c97357ca2"));

            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8e59-af70-731b-81b2-22deb795b880"));

            migrationBuilder.InsertData(
                table: "competitions",
                columns: new[] { "id", "name", "scope", "season", "type" },
                values: new object[,]
                {
                    { new Guid("019a8e5b-792c-74fd-9f4c-4dcba43d6458"), "Premier League", 3, "2025/2026", 1 },
                    { new Guid("019a8e5b-792c-74fe-bbb2-e10d64e8f3d9"), "Brasileirao Serie A", 3, "2025", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8e5b-792c-74fd-9f4c-4dcba43d6458"));

            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8e5b-792c-74fe-bbb2-e10d64e8f3d9"));

            migrationBuilder.InsertData(
                table: "competitions",
                columns: new[] { "id", "name", "scope", "season", "type" },
                values: new object[,]
                {
                    { new Guid("019a8e59-af70-731a-b307-c72c97357ca2"), "Premier League", 3, "2025/2026", 1 },
                    { new Guid("019a8e59-af70-731b-81b2-22deb795b880"), "Brasileirao Serie A", 3, "2025", 1 }
                });
        }
    }
}
