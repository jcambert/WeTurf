using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewpredictionbydate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                @"CREATE OR REPLACE VIEW public.turfpredictionbydate
 AS
 SELECT DISTINCT p.""Date"",
    p.""Reunion"",
    p.""Course"",
    p.""NumeroPmu""
   FROM turfpredicted p
  ORDER BY p.""Date"", p.""Reunion"", p.""Course"", p.""NumeroPmu"";

ALTER TABLE public.turfpredictionbydate
    OWNER TO weturf_root;;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfpredictionbydate");
        }
    }
}
