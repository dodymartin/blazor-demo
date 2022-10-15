using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RushAg.Infrastructure.Data.Migrations
{
    public partial class FixstepsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodoId",
                table: "TodoSteps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TodoId",
                table: "TodoSteps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
