using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTOHelper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedTo",
                table: "DevTasks",
                newName: "Status");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToEmployeeId",
                table: "DevTasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssignedToEmployeeId",
                table: "DevTasks");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DevTasks",
                newName: "AssignedTo");
        }
    }
}
