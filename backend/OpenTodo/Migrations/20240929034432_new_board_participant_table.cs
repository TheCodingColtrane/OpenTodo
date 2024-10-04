using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OpenTodo.Migrations
{
    /// <inheritdoc />
    public partial class new_board_participant_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 28, 19, 50, 14, 896, DateTimeKind.Utc).AddTicks(3224));

            migrationBuilder.CreateTable(
                name: "board_participant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsersId = table.Column<int>(type: "integer", nullable: false),
                    BoardsID = table.Column<int>(type: "integer", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_participant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_board_participant_board_BoardsID",
                        column: x => x.BoardsID,
                        principalTable: "board",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_board_participant_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_board_participant_BoardsID",
                table: "board_participant",
                column: "BoardsID");

            migrationBuilder.CreateIndex(
                name: "IX_board_participant_UsersId",
                table: "board_participant",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_participant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 28, 19, 50, 14, 896, DateTimeKind.Utc).AddTicks(3224),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 29, 3, 44, 31, 859, DateTimeKind.Utc).AddTicks(2826));
        }
    }
}
