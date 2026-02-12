using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingNaviagtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GradeStreamId",
                table: "Assignments",
                column: "GradeStreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_GradeStreams_GradeStreamId",
                table: "Assignments",
                column: "GradeStreamId",
                principalTable: "GradeStreams",
                principalColumn: "StreamId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_GradeStreams_GradeStreamId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_GradeStreamId",
                table: "Assignments");
        }
    }
}
