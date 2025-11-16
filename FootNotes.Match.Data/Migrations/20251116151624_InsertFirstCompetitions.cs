using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FootNotes.MatchManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertFirstCompetitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "competitions",
                columns: new[] { "id", "name", "scope", "season", "type" },
                values: new object[,]
                {
                    { new Guid("019a8d3d-1db6-71f2-ae7c-ed0aaf24ac07"), "Premier League", 3, "2025/2026", 1 },
                    { new Guid("019a8d3d-1db6-71f3-838e-dab6cdb6baae"), "Brasileirao Serie A", 3, "2025", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8d3d-1db6-71f2-ae7c-ed0aaf24ac07"));

            migrationBuilder.DeleteData(
                table: "competitions",
                keyColumn: "id",
                keyValue: new Guid("019a8d3d-1db6-71f3-838e-dab6cdb6baae"));
        }
    }
}
