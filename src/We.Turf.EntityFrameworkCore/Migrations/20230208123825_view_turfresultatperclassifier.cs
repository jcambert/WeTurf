using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturfresultatperclassifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR REPLACE VIEW public.turfresultatperclassifier
 AS
   SELECT r.""Date"",
    r.""Classifier"",
    count(r.""Classifier"") AS counting
   FROM turfresultatofpredicted r
  WHERE r.""Pari"" = 'E_SIMPLE_PLACE'::text
  GROUP BY r.""Date"", r.""Classifier"";

ALTER TABLE public.turfresultatperclassifier
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfresultatperclassifier");
        }
    }
}
