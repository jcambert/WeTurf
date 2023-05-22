using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewresultatofpredictedwithoutclassifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                @"CREATE OR REPLACE VIEW public.turfresultatofpredictedwithoutclassifier
 AS
 SELECT DISTINCT turfresultatofpredicted.""Date"",
    turfresultatofpredicted.""Reunion"",
    turfresultatofpredicted.""Course"",
    turfresultatofpredicted.""NumeroPmu"",
    turfresultatofpredicted.""Pari"",
    turfresultatofpredicted.dividende
   FROM turfresultatofpredicted
  ORDER BY turfresultatofpredicted.""Date"", turfresultatofpredicted.""Reunion"", turfresultatofpredicted.""Course"", turfresultatofpredicted.""NumeroPmu"";

ALTER TABLE public.turfresultatofpredictedwithoutclassifier
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfresultatofpredictedwithoutclassifier");
        }
    }
}
