using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pis_web_api.Migrations
{
    /// <inheritdoc />
    public partial class newAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Animals");
        }
    }
}
