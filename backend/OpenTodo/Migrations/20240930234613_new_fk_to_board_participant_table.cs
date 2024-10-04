using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk_to_board_participant_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant");

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_user_id",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "board_id",
                table: "board_participant",
                newName: "BoardId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "board_participant",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "BoardsID",
                table: "board_participant",
                newName: "Board");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_user_id",
                table: "board_participant",
                newName: "IX_board_participant_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_BoardsID",
                table: "board_participant",
                newName: "IX_board_participant_Board");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant",
                column: "Board",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_user_UsersId",
                table: "board_participant",
                column: "UsersId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant");

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_UsersId",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "board_participant",
                newName: "board_id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "board_participant",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Board",
                table: "board_participant",
                newName: "BoardsID");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_UsersId",
                table: "board_participant",
                newName: "IX_board_participant_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_Board",
                table: "board_participant",
                newName: "IX_board_participant_BoardsID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538));

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant",
                column: "BoardsID",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_user_user_id",
                table: "board_participant",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
