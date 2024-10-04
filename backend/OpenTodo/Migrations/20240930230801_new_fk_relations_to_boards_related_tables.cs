using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk_relations_to_boards_related_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_UserId",
                table: "board");

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "BoardsID",
                table: "board_participant",
                newName: "Board");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_BoardsID",
                table: "board_participant",
                newName: "IX_board_participant_Board");

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
                defaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "board",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_board_user_user_id",
                table: "board",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant",
                column: "Board",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_user_id",
                table: "board");

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "Board",
                table: "board_participant",
                newName: "BoardsID");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_Board",
                table: "board_participant",
                newName: "IX_board_participant_BoardsID");

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
                defaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "board",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_board_user_UserId",
                table: "board",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant",
                column: "BoardsID",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
