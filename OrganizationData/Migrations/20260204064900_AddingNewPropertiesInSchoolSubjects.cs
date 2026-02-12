using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewPropertiesInSchoolSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "SchoolSubjects");

            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "SchoolSubjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "SchoolSubjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubjects_GradeId",
                table: "SchoolSubjects",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSubjects_Grades_GradeId",
                table: "SchoolSubjects",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSubjects_Grades_GradeId",
                table: "SchoolSubjects");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSubjects_GradeId",
                table: "SchoolSubjects");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "SchoolSubjects");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "SchoolSubjects");

            migrationBuilder.AddColumn<string>(
                name: "GradeLevel",
                table: "SchoolSubjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
