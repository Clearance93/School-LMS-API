using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class PragiarismDetection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlagiarismResults",
                columns: table => new
                {
                    PlagiarismResultsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlagiarismPerecentage = table.Column<int>(type: "int", nullable: false),
                    AIGeneratedPercentage = table.Column<int>(type: "int", nullable: false),
                    OriginalWorkPercentage = table.Column<int>(type: "int", nullable: false),
                    PlagiarismSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AIDetectionSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverallVerdict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchedSourceJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkingResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlagiarismResults", x => x.PlagiarismResultsId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlagiarismResults");
        }
    }
}
