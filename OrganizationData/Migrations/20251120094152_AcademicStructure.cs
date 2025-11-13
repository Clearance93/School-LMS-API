using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AcademicStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId");
                });

            migrationBuilder.CreateTable(
                name: "GradeStreams",
                columns: table => new
                {
                    StreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeStreams", x => x.StreamId);
                    table.ForeignKey(
                        name: "FK_GradeStreams_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeStreams_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId");
                    table.ForeignKey(
                        name: "FK_GradeStreams_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_OrganizationId",
                table: "Grades",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeStreams_GradeId",
                table: "GradeStreams",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeStreams_OrganizationId",
                table: "GradeStreams",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeStreams_TeacherId",
                table: "GradeStreams",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeStreams");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
