using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addparameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turfparameter",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        BaseScrapFolder = table.Column<string>(type: "text", nullable: true),
                        InputScrapFolder = table.Column<string>(type: "text", nullable: true),
                        OutputScrapFolder = table.Column<string>(type: "text", nullable: true),
                        PredictionFilename = table.Column<string>(type: "text", nullable: true),
                        CourseFilename = table.Column<string>(type: "text", nullable: true),
                        ScrapInFolder = table.Column<bool>(type: "boolean", nullable: false),
                        InputScrapFolderMonthPattern = table.Column<string>(
                            type: "text",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turfparameter", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "turfparameter");
        }
    }
}
