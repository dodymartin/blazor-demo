using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RushAg.Server.Migrations
{
    public partial class TodoItemAddNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TodoItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TodoItems");
        }
    }
}
