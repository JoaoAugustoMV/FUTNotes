using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootNotes.Annotations.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "annotation_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    match_id = table.Column<Guid>(type: "uuid", nullable: false),
                    started = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_sessions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "annotation_tags",
                columns: table => new
                {
                    annotation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_tags", x => new { x.annotation_id, x.tag_id });
                });

            migrationBuilder.CreateTable(
                name: "annotations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    annotation_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<byte>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    minute = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotations", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotations_annotation_sessions_annotation_session_id",
                        column: x => x.annotation_session_id,
                        principalTable: "annotation_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnnotationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_annotations_AnnotationId",
                        column: x => x.AnnotationId,
                        principalTable: "annotations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_annotations_annotation_session_id",
                table: "annotations",
                column: "annotation_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_tags_AnnotationId",
                table: "tags",
                column: "AnnotationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annotation_tags");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "annotations");

            migrationBuilder.DropTable(
                name: "annotation_sessions");
        }
    }
}
