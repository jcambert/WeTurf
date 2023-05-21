using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewstatbydate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                @"CREATE OR REPLACE VIEW public.turfstatbydate
 AS
 SELECT turfresultatofpredicted.""Date"", turfresultatofpredicted.""Classifier"",
    turfresultatofpredicted.""Pari"",
    count(turfresultatofpredicted.""Classifier"") AS ""Mise"",
    sum(turfresultatofpredicted.dividende) AS ""Dividende""
   FROM turfresultatofpredicted
  GROUP BY turfresultatofpredicted.""Date"", turfresultatofpredicted.""Classifier"", turfresultatofpredicted.""Pari""
  ORDER BY turfresultatofpredicted.""Date"",turfresultatofpredicted.""Classifier"";

ALTER TABLE public.turfstat
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfstatbydate");
        }
    }
}
