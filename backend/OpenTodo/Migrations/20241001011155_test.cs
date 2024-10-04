using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant");

            migrationBuilder.DropIndex(
                name: "IX_board_participant_Board",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "Board",
                table: "board_participant",
                newName: "UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.AddColumn<int>(
                name: "BoardsID",
                table: "board_participant",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_board_participant_BoardsID",
                table: "board_participant",
                column: "BoardsID");

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant",
                column: "BoardsID",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_board_BoardsID",
                table: "board_participant");

            migrationBuilder.DropIndex(
                name: "IX_board_participant_BoardsID",
                table: "board_participant");

            migrationBuilder.DropColumn(
                name: "BoardsID",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "board_participant",
                newName: "Board");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 1, 11, 55, 141, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.CreateIndex(
                name: "IX_board_participant_Board",
                table: "board_participant",
                column: "Board");

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_board_Board",
                table: "board_participant",
                column: "Board",
                principalTable: "board",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
