using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addcourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turfcourse",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Reunion = table.Column<int>(type: "integer", nullable: false),
                        Numero = table.Column<int>(type: "integer", nullable: false),
                        Libelle = table.Column<string>(type: "text", nullable: true),
                        LibelleCourt = table.Column<string>(type: "text", nullable: true),
                        MontantPrix = table.Column<int>(type: "integer", nullable: false),
                        Distance = table.Column<int>(type: "integer", nullable: false),
                        DistanceUnite = table.Column<string>(type: "text", nullable: true),
                        Discipline = table.Column<string>(type: "text", nullable: true),
                        Specialite = table.Column<string>(type: "text", nullable: true),
                        NombrePartants = table.Column<int>(type: "integer", nullable: false),
                        OrdreArrivee = table.Column<string>(type: "text", nullable: true),
                        HippoCode = table.Column<string>(type: "text", nullable: true),
                        HippoCourt = table.Column<string>(type: "text", nullable: true),
                        HippoLong = table.Column<string>(type: "text", nullable: true),
                        Date = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turfcourse", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_turfcourse_Date",
                table: "turfcourse",
                column: "Date"
            );

            migrationBuilder.CreateIndex(
                name: "IX_turfcourse_HippoCode",
                table: "turfcourse",
                column: "HippoCode"
            );

            migrationBuilder.CreateIndex(
                name: "IX_turfcourse_Numero",
                table: "turfcourse",
                column: "Numero"
            );

            migrationBuilder.CreateIndex(
                name: "IX_turfcourse_Reunion",
                table: "turfcourse",
                column: "Reunion"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "turfcourse");
        }
    }
}
