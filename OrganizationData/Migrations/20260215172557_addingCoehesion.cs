using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class addingCoehesion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop");

            migrationBuilder.AlterColumn<Guid>(
                name: "GradeId",
                table: "ScheduledWorkshop",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop");

            migrationBuilder.AlterColumn<Guid>(
                name: "GradeId",
                table: "ScheduledWorkshop",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkshop_Grades_GradeId",
                table: "ScheduledWorkshop",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
