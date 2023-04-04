using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addprogrammecourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR REPLACE VIEW public.turfprogrammecourse
 AS
 SELECT turfcourse.""Id"",
    turfcourse.""Date"",
    turfcourse.""Reunion"",
    turfcourse.""Numero"",
    turfcourse.""Discipline"",
    turfcourse.""HippoCourt"",
    turfcourse.""Libelle"",
    turfcourse.""Distance"",
    turfcourse.""DistanceUnite"",
    turfcourse.""NombrePartants"",
    turfcourse.""OrdreArrivee""
   FROM turfcourse
  ORDER BY turfcourse.""Date"", turfcourse.""Reunion"", turfcourse.""Numero"";

ALTER TABLE public.turfprogrammecourse
    OWNER TO weturf_root;";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfprogrammecourse;");
        }
    }
}
