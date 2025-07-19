using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTOHelper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndTeamEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "TaskItems");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToUserId",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToUserId",
                table: "DevTasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DevTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "DevTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DevTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeamLeadId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_AssignedToId",
                table: "TaskItems",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_DevTasks_AssignedToUserId",
                table: "DevTasks",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DevTasks_CreatedByUserId",
                table: "DevTasks",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeadId",
                table: "Teams",
                column: "TeamLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DevTasks_Users_AssignedToUserId",
                table: "DevTasks",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DevTasks_Users_CreatedByUserId",
                table: "DevTasks",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_AssignedToId",
                table: "TaskItems",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevTasks_Users_AssignedToUserId",
                table: "DevTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_DevTasks_Users_CreatedByUserId",
                table: "DevTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_AssignedToId",
                table: "TaskItems");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_AssignedToId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_DevTasks_AssignedToUserId",
                table: "DevTasks");

            migrationBuilder.DropIndex(
                name: "IX_DevTasks_CreatedByUserId",
                table: "DevTasks");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "DevTasks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DevTasks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "DevTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DevTasks");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
