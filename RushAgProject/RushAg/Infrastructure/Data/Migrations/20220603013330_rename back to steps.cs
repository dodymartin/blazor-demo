using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RushAg.Infrastructure.Data.Migrations
{
    public partial class renamebacktosteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoStep_TodoItems_TodoItemId",
                table: "TodoStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoStep",
                table: "TodoStep");

            migrationBuilder.RenameTable(
                name: "TodoStep",
                newName: "TodoSteps");

            migrationBuilder.RenameIndex(
                name: "IX_TodoStep_TodoItemId",
                table: "TodoSteps",
                newName: "IX_TodoSteps_TodoItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoSteps",
                table: "TodoSteps",
                column: "TodoStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps",
                column: "TodoItemId",
                principalTable: "TodoItems",
                principalColumn: "TodoItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoSteps_TodoItems_TodoItemId",
                table: "TodoSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoSteps",
                table: "TodoSteps");

            migrationBuilder.RenameTable(
                name: "TodoSteps",
                newName: "TodoStep");

            migrationBuilder.RenameIndex(
                name: "IX_TodoSteps_TodoItemId",
                table: "TodoStep",
                newName: "IX_TodoStep_TodoItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoStep",
                table: "TodoStep",
                column: "TodoStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoStep_TodoItems_TodoItemId",
                table: "TodoStep",
                column: "TodoItemId",
                principalTable: "TodoItems",
                principalColumn: "TodoItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
