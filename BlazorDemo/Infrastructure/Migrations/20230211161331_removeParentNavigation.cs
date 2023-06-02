using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorDemo.Infrastructure.Migrations
{
    public partial class removeParentNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps");

            migrationBuilder.AlterColumn<long>(
                name: "TodoItemId",
                table: "TodoSteps",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps",
                column: "TodoItemId",
                principalTable: "TodoItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps");

            migrationBuilder.AlterColumn<long>(
                name: "TodoItemId",
                table: "TodoSteps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps",
                column: "TodoItemId",
                principalTable: "TodoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
