using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk_across_the_board : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_user_id",
                table: "board");

            migrationBuilder.DropForeignKey(
                name: "FK_category_board_BoardID",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_task_board_BoardID",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "FK_task_user_AssignedUserId",
                table: "task");

            migrationBuilder.RenameColumn(
                name: "BoardID",
                table: "task",
                newName: "board_id");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "task",
                newName: "assigned_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_task_BoardID",
                table: "task",
                newName: "IX_task_board_id");

            migrationBuilder.RenameIndex(
                name: "IX_task_AssignedUserId",
                table: "task",
                newName: "IX_task_assigned_user_id");

            migrationBuilder.RenameColumn(
                name: "BoardID",
                table: "category",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_category_BoardID",
                table: "category",
                newName: "IX_category_BoardId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "board",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_board_user_id",
                table: "board",
                newName: "IX_board_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "task",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.AddColumn<int>(
                name: "creator_user_id",
                table: "board",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_board_user_UserId",
                table: "board",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_category_board_BoardId",
                table: "category",
                column: "BoardId",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_board_board_id",
                table: "task",
                column: "board_id",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_user_assigned_user_id",
                table: "task",
                column: "assigned_user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_UserId",
                table: "board");

            migrationBuilder.DropForeignKey(
                name: "FK_category_board_BoardId",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_task_board_board_id",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "FK_task_user_assigned_user_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "creator_user_id",
                table: "board");

            migrationBuilder.RenameColumn(
                name: "board_id",
                table: "task",
                newName: "BoardID");

            migrationBuilder.RenameColumn(
                name: "assigned_user_id",
                table: "task",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_task_board_id",
                table: "task",
                newName: "IX_task_BoardID");

            migrationBuilder.RenameIndex(
                name: "IX_task_assigned_user_id",
                table: "task",
                newName: "IX_task_AssignedUserId");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "category",
                newName: "BoardID");

            migrationBuilder.RenameIndex(
                name: "IX_category_BoardId",
                table: "category",
                newName: "IX_category_BoardID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "board",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_board_UserId",
                table: "board",
                newName: "IX_board_user_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636));

            migrationBuilder.AddForeignKey(
                name: "FK_board_user_user_id",
                table: "board",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_category_board_BoardID",
                table: "category",
                column: "BoardID",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_board_BoardID",
                table: "task",
                column: "BoardID",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_user_AssignedUserId",
                table: "task",
                column: "AssignedUserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
