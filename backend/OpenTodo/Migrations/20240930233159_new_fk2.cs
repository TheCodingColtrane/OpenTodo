using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk2 : Migration
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
                newName: "board_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61));

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
                name: "board_id",
                table: "board_participant",
                newName: "Board");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 31, 59, 259, DateTimeKind.Utc).AddTicks(8284));

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
