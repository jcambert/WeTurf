using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class resultat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turfresultat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Reunion = table.Column<int>(type: "integer", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    Pari = table.Column<string>(type: "text", nullable: true),
                    NumeroPmu = table.Column<int>(type: "integer", nullable: false),
                    Rapport = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turfresultat", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turfresultat_Course",
                table: "turfresultat",
                column: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_turfresultat_Date",
                table: "turfresultat",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_turfresultat_Reunion",
                table: "turfresultat",
                column: "Reunion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "turfresultat");
        }
    }
}
