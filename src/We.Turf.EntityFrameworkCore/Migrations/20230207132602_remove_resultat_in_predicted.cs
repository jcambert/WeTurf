using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class removeresultatinpredicted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DividenceGagnant", table: "turfpredicted");

            migrationBuilder.DropColumn(name: "DividencePlace", table: "turfpredicted");

            migrationBuilder.DropColumn(name: "Place", table: "turfpredicted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DividenceGagnant",
                table: "turfpredicted",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0
            );

            migrationBuilder.AddColumn<double>(
                name: "DividencePlace",
                table: "turfpredicted",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0
            );

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "turfpredicted",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}
