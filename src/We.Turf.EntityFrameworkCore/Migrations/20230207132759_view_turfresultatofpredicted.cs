using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturfresultatofpredicted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR REPLACE VIEW public.turfresultatofpredicted
 AS
 SELECT p.""Id"",
    p.""Classifier"",
    p.""Date"",
    p.""Reunion"",
    p.""Course"",
    p.""NumeroPmu"",
    p.""Nom"",
    p.""Rapport"",
    p.""Specialite"",
    p.""Hippodrome"",
    r.""Id"" AS resultat_id,
    r.""Pari"",
    r.""Rapport"" AS dividende
   FROM turfpredicted p,
    turfresultat r
  WHERE p.""Date"" = r.""Date"" AND p.""Reunion"" = r.""Reunion"" AND p.""Course"" = r.""Course"" AND p.""NumeroPmu"" = r.""NumeroPmu"";

ALTER TABLE public.turfresultatofpredicted
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfresultatofpredicted");
        }
    }
}
