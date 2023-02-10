using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturfpredictionperclassifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR REPLACE VIEW public.turfpredictionperclassifier
 AS
  SELECT p.""Classifier"",
    count(p.""Classifier"") AS counting
   FROM public.turfpredicted p
  GROUP BY p.""Classifier"";

ALTER TABLE public.turfpredictionperclassifier
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfpredictionperclassifier");
        }
    }
}
