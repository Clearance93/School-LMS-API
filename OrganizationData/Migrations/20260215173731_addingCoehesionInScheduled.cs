using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class addingCoehesionInScheduled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkshop_GradeId",
                table: "ScheduledWorkshop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkshop_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");
        }
    }
}
