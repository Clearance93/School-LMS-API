using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingThreeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BroadcastMessage",
                columns: table => new
                {
                    BroadcastId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroadcastMessage", x => x.BroadcastId);
                    table.ForeignKey(
                        name: "FK_BroadcastMessage_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreateMessage",
                columns: table => new
                {
                    CreateMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipientRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateMessage", x => x.CreateMessageId);
                    table.ForeignKey(
                        name: "FK_CreateMessage_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_OrganizationSetup_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSetup",
                        principalColumn: "OrganizationSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BroadcastMessage_OrganizationId",
                table: "BroadcastMessage",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateMessage_OrganizationId",
                table: "CreateMessage",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_OrganizationId",
                table: "Message",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BroadcastMessage");

            migrationBuilder.DropTable(
                name: "CreateMessage");

            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
