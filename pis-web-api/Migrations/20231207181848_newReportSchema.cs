using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pis_web_api.Migrations
{
    /// <inheritdoc />
    public partial class newReportSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Reports");

            migrationBuilder.CreateTable(
                name: "StatisticaHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocalityName = table.Column<string>(type: "text", nullable: false),
                    ReportId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticaHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticaHolder_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StatisticItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VaccineName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StatisticaHolderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticItems_StatisticaHolder_StatisticaHolderId",
                        column: x => x.StatisticaHolderId,
                        principalTable: "StatisticaHolder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticaHolder_ReportId",
                table: "StatisticaHolder",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticItems_StatisticaHolderId",
                table: "StatisticItems",
                column: "StatisticaHolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticItems");

            migrationBuilder.DropTable(
                name: "StatisticaHolder");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
