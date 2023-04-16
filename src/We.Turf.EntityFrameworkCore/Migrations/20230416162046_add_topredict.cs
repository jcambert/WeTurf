using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addtopredict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turftopredict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Reunion = table.Column<int>(type: "integer", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    HippoCode = table.Column<string>(type: "text", nullable: true),
                    HippoNom = table.Column<string>(type: "text", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: true),
                    NumeroPmu = table.Column<int>(type: "integer", nullable: false),
                    Rapport = table.Column<double>(type: "double precision", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Sexe = table.Column<string>(type: "text", nullable: true),
                    Race = table.Column<string>(type: "text", nullable: true),
                    Statut = table.Column<string>(type: "text", nullable: true),
                    Oeilleres = table.Column<string>(type: "text", nullable: true),
                    Deferre = table.Column<string>(type: "text", nullable: true),
                    IndicateurInedit = table.Column<string>(type: "text", nullable: true),
                    Musique = table.Column<string>(type: "text", nullable: true),
                    NombreCourses = table.Column<int>(type: "integer", nullable: false),
                    NombreVictoires = table.Column<int>(type: "integer", nullable: false),
                    NombrePlaces = table.Column<int>(type: "integer", nullable: false),
                    NombrePlacesSecond = table.Column<int>(type: "integer", nullable: false),
                    NombrePlacesTroisieme = table.Column<int>(type: "integer", nullable: false),
                    OrdreArrivee = table.Column<int>(type: "integer", nullable: false),
                    Distance = table.Column<int>(type: "integer", nullable: false),
                    HadicapDistance = table.Column<int>(type: "integer", nullable: false),
                    GainsCarriere = table.Column<int>(type: "integer", nullable: false),
                    GainsVictoires = table.Column<int>(type: "integer", nullable: false),
                    GainsPlaces = table.Column<int>(type: "integer", nullable: false),
                    GainsAnneeEnCours = table.Column<int>(type: "integer", nullable: false),
                    GainsAnneePrecedente = table.Column<int>(type: "integer", nullable: false),
                    PlaceCorde = table.Column<int>(type: "integer", nullable: false),
                    HadicapValeur = table.Column<int>(type: "integer", nullable: false),
                    HandicapPoids = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turftopredict", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turftopredict_Course",
                table: "turftopredict",
                column: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_turftopredict_Date",
                table: "turftopredict",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_turftopredict_Reunion",
                table: "turftopredict",
                column: "Reunion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "turftopredict");
        }
    }
}
