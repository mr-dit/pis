using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pis_web_api.Migrations
{
    /// <inheritdoc />
    public partial class newReportSchema2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatisticaHolder_Reports_ReportId",
                table: "StatisticaHolder");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticItems_StatisticaHolder_StatisticaHolderId",
                table: "StatisticItems");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticaHolderId",
                table: "StatisticItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "StatisticaHolder",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticaHolder_Reports_ReportId",
                table: "StatisticaHolder",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticItems_StatisticaHolder_StatisticaHolderId",
                table: "StatisticItems",
                column: "StatisticaHolderId",
                principalTable: "StatisticaHolder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatisticaHolder_Reports_ReportId",
                table: "StatisticaHolder");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticItems_StatisticaHolder_StatisticaHolderId",
                table: "StatisticItems");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticaHolderId",
                table: "StatisticItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "StatisticaHolder",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticaHolder_Reports_ReportId",
                table: "StatisticaHolder",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticItems_StatisticaHolder_StatisticaHolderId",
                table: "StatisticItems",
                column: "StatisticaHolderId",
                principalTable: "StatisticaHolder",
                principalColumn: "Id");
        }
    }
}
