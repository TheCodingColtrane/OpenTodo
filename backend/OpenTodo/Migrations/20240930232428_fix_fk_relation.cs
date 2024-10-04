using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class fix_fk_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_user_user_id",
                table: "board");

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_UsersId",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "board_participant",
                newName: "board_id");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_UsersId",
                table: "board_participant",
                newName: "IX_board_participant_board_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480));

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "board",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_board_user_user_id",
                table: "board",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_user_board_id",
                table: "board_participant",
                column: "board_id",
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

            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_board_id",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "board_id",
                table: "board_participant",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_board_id",
                table: "board_participant",
                newName: "IX_board_participant_UsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 8, 0, 267, DateTimeKind.Utc).AddTicks(8480),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151));

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
                name: "FK_board_participant_user_UsersId",
                table: "board_participant",
                column: "UsersId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
