using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class viewturflastscrappeddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //SELECT distinct max(p."Date") as LastScrappedDate FROM public.turfpredicted p
            var sql = @"CREATE OR REPLACE VIEW public.turfllastscrapped
 AS
 SELECT DISTINCT max(p.""Date"") AS lastscrappeddate
   FROM turfpredicted p;

ALTER TABLE public.turfllastscrapped
    OWNER TO weturf_root;";
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfllastscrapped");
        }
    }
}
