using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListAppData.Migrations
{
    public partial class AddFkTodoTasksAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "TodoTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_StatusId",
                table: "TodoTasks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTasks_Statuses_StatusId",
                table: "TodoTasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTasks_Statuses_StatusId",
                table: "TodoTasks");

            migrationBuilder.DropIndex(
                name: "IX_TodoTasks_StatusId",
                table: "TodoTasks");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "TodoTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
