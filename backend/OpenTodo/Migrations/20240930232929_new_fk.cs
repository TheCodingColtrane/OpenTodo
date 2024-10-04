using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_board_id",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "board_id",
                table: "board_participant",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_board_id",
                table: "board_participant",
                newName: "IX_board_participant_user_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_user_user_id",
                table: "board_participant",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_participant_user_user_id",
                table: "board_participant");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "board_participant",
                newName: "board_id");

            migrationBuilder.RenameIndex(
                name: "IX_board_participant_user_id",
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
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 24, 27, 601, DateTimeKind.Utc).AddTicks(4151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 29, 29, 217, DateTimeKind.Utc).AddTicks(61));

            migrationBuilder.AddForeignKey(
                name: "FK_board_participant_user_board_id",
                table: "board_participant",
                column: "board_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
