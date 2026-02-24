using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class updatePlagiarismTableSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlagiarismPerecentage",
                table: "PlagiarismResults",
                newName: "PlagiarismPercentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlagiarismPercentage",
                table: "PlagiarismResults",
                newName: "PlagiarismPerecentage");
        }
    }
}
