using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FootNotes.MatchManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColHasCreatedManuallyOnMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019ab19c-bc45-7179-be31-5546aa9f5ba3"));

            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019ab19c-bc45-717a-8ec5-1d00a3aa5f43"));

            migrationBuilder.RenameColumn(
                name: "HasCreatedManually",
                table: "matches",
                newName: "has_created_manually");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "has_created_manually",
                table: "matches",
                newName: "HasCreatedManually");

            migrationBuilder.InsertData(
                table: "competitions",
                columns: new[] { "id", "name", "scope", "season", "type" },
                values: new object[,]
                {
                    { new Guid("019ab19c-bc45-7179-be31-5546aa9f5ba3"), "Premier League", 3, "2025/2026", 1 },
                    { new Guid("019ab19c-bc45-717a-8ec5-1d00a3aa5f43"), "Brasileirao Serie A", 3, "2025", 1 }
                });
        }
    }
}
