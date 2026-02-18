using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingGradeRelationshipInTheTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "ScheduledWorkshop",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkshop_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkshop_GradeId",
                table: "ScheduledWorkshop");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "ScheduledWorkshop");
        }
    }
}
