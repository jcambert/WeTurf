using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturfgetlastdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
CREATE OR REPLACE VIEW public.turfgetlastpredicationdate
 AS
 SELECT DISTINCT min(p.""Date"") AS datedebut,
    max(p.""Date"") AS datefin
   FROM turfpredicted p;

ALTER TABLE public.turfgetlastpredicationdate
    OWNER TO weturf_root;


CREATE OR REPLACE VIEW public.turfgetlastresultatdate
 AS
  SELECT DISTINCT min(p.""Date"") AS datedebut,
    max(p.""Date"") AS datefin
   FROM turfresultat p;

ALTER TABLE public.turfgetlastresultatdate
    OWNER TO weturf_root;
";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfgetlastpredicationdate;DROP VIEW  turfgetlastresultatdate");
        }
    }
}
