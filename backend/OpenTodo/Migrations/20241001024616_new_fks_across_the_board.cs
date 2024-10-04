using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fks_across_the_board : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_UserId",
                table: "board");

            migrationBuilder.DropColumn(
                name: "creator_user_id",
                table: "board");

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
                defaultValue: new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_user_id",
                table: "board");

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
                oldDefaultValue: new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 52, 28, 378, DateTimeKind.Utc).AddTicks(4636),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 2, 46, 16, 198, DateTimeKind.Utc).AddTicks(6123));

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
        }
    }
}
