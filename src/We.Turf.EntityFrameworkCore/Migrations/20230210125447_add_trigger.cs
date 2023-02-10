using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addtrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turfscraptrigger",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turfscraptrigger", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turfscraptrigger_Start",
                table: "turfscraptrigger",
                column: "Start");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "turfscraptrigger");
        }
    }
}
