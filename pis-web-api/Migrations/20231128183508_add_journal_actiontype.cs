using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pis_web_api.Migrations
{
    /// <inheritdoc />
    public partial class add_journal_actiontype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionType",
                table: "Journals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Journals");
        }
    }
}
