using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingExamsGradesScales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamGradeScale",
                columns: table => new
                {
                    ExamGradeScaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassMark = table.Column<int>(type: "int", nullable: true),
                    ExcellentMark = table.Column<int>(type: "int", nullable: true),
                    AverageMark = table.Column<int>(type: "int", nullable: true),
                    PoorMark = table.Column<int>(type: "int", nullable: true),
                    DistinctionMark = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamGradeScale", x => x.ExamGradeScaleId);
                    table.ForeignKey(
                        name: "FK_ExamGradeScale_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamGradeScale_OrganizationId",
                table: "ExamGradeScale",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamGradeScale");
        }
    }
}
