using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingTwoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseStreams",
                columns: table => new
                {
                    CourseStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseStreamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStreams", x => x.CourseStreamId);
                    table.ForeignKey(
                        name: "FK_CourseStreams_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeLevel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSubjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_SchoolSubjects_CourseStreams_CourseStreamId",
                        column: x => x.CourseStreamId,
                        principalTable: "CourseStreams",
                        principalColumn: "CourseStreamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStreams_OrganizationId",
                table: "CourseStreams",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubjects_CourseStreamId",
                table: "SchoolSubjects",
                column: "CourseStreamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolSubjects");

            migrationBuilder.DropTable(
                name: "CourseStreams");
        }
    }
}
