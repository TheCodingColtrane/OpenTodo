using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_fk_to_board_participants_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "board_participant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 23, 46, 12, 712, DateTimeKind.Utc).AddTicks(5538),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 1, 0, 0, 47, 386, DateTimeKind.Utc).AddTicks(567));
        }
    }
}
