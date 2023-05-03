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
            var sql =
                @"CREATE OR REPLACE VIEW public.turfllastscrapped
 AS
 SELECT DISTINCT max(p.""Date"") AS lastscrappeddate
   FROM turfpredicted p;

ALTER TABLE public.turfllastscrapped
    OWNER TO weturf_root;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfllastscrapped");
        }
    }
}
