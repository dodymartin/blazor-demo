using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RushAg.Infrastructure.Data.Migrations
{
    public partial class CompleteDatespellfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletDate",
                table: "TodoItems",
                newName: "CompleteDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompleteDate",
                table: "TodoItems",
                newName: "CompletDate");
        }
    }
}
