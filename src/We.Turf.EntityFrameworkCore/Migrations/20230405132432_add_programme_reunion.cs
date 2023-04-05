using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace We.Turf.Migrations
{
    /// <inheritdoc />
    public partial class addprogrammereunion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR REPLACE VIEW public.turfprogrammereunion
 AS
 SELECT DISTINCT turfcourse.""Date"",
    turfcourse.""Reunion"",
    turfcourse.""HippoCourt"",
    turfcourse.""Discipline"",
    count(turfcourse.""Numero"") AS count
   FROM turfcourse
  GROUP BY turfcourse.""Date"", turfcourse.""Reunion"", turfcourse.""HippoCourt"", turfcourse.""Discipline""
  ORDER BY turfcourse.""Date"", turfcourse.""Reunion"";

ALTER TABLE public.turfprogrammereunion
    OWNER TO weturf_root;";

            migrationBuilder.Sql(sql);  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW turfprogrammereunion;");
        }
    }
}
