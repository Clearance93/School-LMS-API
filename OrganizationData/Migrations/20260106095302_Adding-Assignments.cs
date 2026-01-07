using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceSessions_ClassSchedules_ClassScheduleId",
                table: "AttendanceSessions");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceSessions_ClassScheduleId",
                table: "AttendanceSessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "AttendanceSessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassScheduleId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignmentMarks = table.Column<int>(type: "int", nullable: false),
                    GradeStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignmentFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSubmissions",
                columns: table => new
                {
                    AssignmentSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentPdfSubmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmissions", x => x.AssignmentSubmissionId);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGrades",
                columns: table => new
                {
                    AssignmentGradesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    GradedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGrades", x => x.AssignmentGradesId);
                    table.ForeignKey(
                        name: "FK_AssignmentGrades_AssignmentSubmissions_AssignmentSubmissionId",
                        column: x => x.AssignmentSubmissionId,
                        principalTable: "AssignmentSubmissions",
                        principalColumn: "AssignmentSubmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_ClassScheduleId",
                table: "AttendanceSessions",
                column: "ClassScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGrades_AssignmentSubmissionId",
                table: "AssignmentGrades",
                column: "AssignmentSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_AssignmentId",
                table: "AssignmentSubmissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_StudentId",
                table: "AssignmentSubmissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceSessions_ClassSchedules_ClassScheduleId",
                table: "AttendanceSessions",
                column: "ClassScheduleId",
                principalTable: "ClassSchedules",
                principalColumn: "ClassScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceSessions_ClassSchedules_ClassScheduleId",
                table: "AttendanceSessions");

            migrationBuilder.DropTable(
                name: "AssignmentGrades");

            migrationBuilder.DropTable(
                name: "AssignmentSubmissions");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceSessions_ClassScheduleId",
                table: "AttendanceSessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "AttendanceSessions",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassScheduleId",
                table: "AttendanceSessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_ClassScheduleId",
                table: "AttendanceSessions",
                column: "ClassScheduleId",
                unique: true,
                filter: "[ClassScheduleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceSessions_ClassSchedules_ClassScheduleId",
                table: "AttendanceSessions",
                column: "ClassScheduleId",
                principalTable: "ClassSchedules",
                principalColumn: "ClassScheduleId");
        }
    }
}
