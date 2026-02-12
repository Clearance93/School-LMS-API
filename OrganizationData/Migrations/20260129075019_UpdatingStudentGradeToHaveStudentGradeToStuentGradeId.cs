using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingStudentGradeToHaveStudentGradeToStuentGradeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentGrade",
                table: "StudentsGrade",
                newName: "StudentGradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentGradeId",
                table: "StudentsGrade",
                newName: "StudentGrade");
        }
    }
}
