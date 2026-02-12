using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class RemovingStreamGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSubjects_CourseStreams_CourseStreamId",
                table: "SchoolSubjects");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSubjects_CourseStreamId",
                table: "SchoolSubjects");

            migrationBuilder.DropColumn(
                name: "CourseStreamId",
                table: "SchoolSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseStreamId",
                table: "SchoolSubjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubjects_CourseStreamId",
                table: "SchoolSubjects",
                column: "CourseStreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSubjects_CourseStreams_CourseStreamId",
                table: "SchoolSubjects",
                column: "CourseStreamId",
                principalTable: "CourseStreams",
                principalColumn: "CourseStreamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
