using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RushAg.Infrastructure.Data.Migrations
{
    public partial class changedtheids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TodoStep",
                newName: "TodoStepId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TodoItems",
                newName: "TodoItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TodoStepId",
                table: "TodoStep",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TodoItemId",
                table: "TodoItems",
                newName: "Id");
        }
    }
}
