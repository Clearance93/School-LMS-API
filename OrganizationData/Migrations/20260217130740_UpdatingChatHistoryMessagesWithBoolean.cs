using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingChatHistoryMessagesWithBoolean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBroadcast",
                table: "BackToBackCommunication",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "BackToBackCommunication",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBroadcast",
                table: "BackToBackCommunication");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "BackToBackCommunication");
        }
    }
}
