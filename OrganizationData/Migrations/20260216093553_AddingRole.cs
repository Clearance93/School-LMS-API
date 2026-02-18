using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ScheduledWorkshop",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ScheduledWorkshop");
        }
    }
}
