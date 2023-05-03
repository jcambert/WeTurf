using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturfaccuracyperclassifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                @"
CREATE OR REPLACE VIEW public.turfaccuracyperclassifier
 AS
 SELECT p.""Classifier"" AS classifier,
    sum(p.counting) AS predictioncounting,
    sum(r.counting) AS resultatcounting,
    sum(r.counting) / sum(p.counting) AS accuracy
   FROM turfpredictionperclassifier p
     JOIN turfresultatperclassifier r ON p.""Date"" = r.""Date"" AND p.""Classifier"" = r.""Classifier""
  GROUP BY p.""Classifier"";

ALTER TABLE public.turfaccuracyperclassifier
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfaccuracyperclassifier");
        }
    }
}
