using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationData.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewTeacherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_OrganizationSetup_OrganizationSetupId",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationSetupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "GradeStreamId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeachingClass",
                columns: table => new
                {
                    TeachingClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingClass", x => x.TeachingClassId);
                    table.ForeignKey(
                        name: "FK_TeachingClass_GradeStreams_GradeStreamId",
                        column: x => x.GradeStreamId,
                        principalTable: "GradeStreams",
                        principalColumn: "StreamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedules",
                columns: table => new
                {
                    ClassScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    GradeStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TeachingClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedules", x => x.ClassScheduleId);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_GradeStreams_GradeStreamId",
                        column: x => x.GradeStreamId,
                        principalTable: "GradeStreams",
                        principalColumn: "StreamId");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_TeachingClass_TeachingClassId",
                        column: x => x.TeachingClassId,
                        principalTable: "TeachingClass",
                        principalColumn: "TeachingClassId");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSessions",
                columns: table => new
                {
                    AttendanceSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    ClassScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSessions", x => x.AttendanceSessionId);
                    table.ForeignKey(
                        name: "FK_AttendanceSessions_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "ClassScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendances",
                columns: table => new
                {
                    StudentAttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendances", x => x.StudentAttendanceId);
                    table.ForeignKey(
                        name: "FK_StudentAttendances_AttendanceSessions_AttendanceSessionId",
                        column: x => x.AttendanceSessionId,
                        principalTable: "AttendanceSessions",
                        principalColumn: "AttendanceSessionId");
                    table.ForeignKey(
                        name: "FK_StudentAttendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeStreamId",
                table: "Students",
                column: "GradeStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_ClassScheduleId",
                table: "AttendanceSessions",
                column: "ClassScheduleId",
                unique: true,
                filter: "[ClassScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_GradeStreamId",
                table: "ClassSchedules",
                column: "GradeStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_TeachingClassId",
                table: "ClassSchedules",
                column: "TeachingClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_AttendanceSessionId",
                table: "StudentAttendances",
                column: "AttendanceSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_StudentId",
                table: "StudentAttendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingClass_GradeStreamId",
                table: "TeachingClass",
                column: "GradeStreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_GradeStreams_GradeStreamId",
                table: "Students",
                column: "GradeStreamId",
                principalTable: "GradeStreams",
                principalColumn: "StreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OrganizationSetup_OrganizationSetupId",
                table: "Students",
                column: "OrganizationSetupId",
                principalTable: "OrganizationSetup",
                principalColumn: "OrganizationSetupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_GradeStreams_GradeStreamId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OrganizationSetup_OrganizationSetupId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentAttendances");

            migrationBuilder.DropTable(
                name: "AttendanceSessions");

            migrationBuilder.DropTable(
                name: "ClassSchedules");

            migrationBuilder.DropTable(
                name: "TeachingClass");

            migrationBuilder.DropIndex(
                name: "IX_Students_GradeStreamId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GradeStreamId",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationSetupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OrganizationSetup_OrganizationSetupId",
                table: "Students",
                column: "OrganizationSetupId",
                principalTable: "OrganizationSetup",
                principalColumn: "OrganizationSetupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
