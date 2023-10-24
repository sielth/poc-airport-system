using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardingService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusOnBoarding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Boardings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Boardings");
        }
    }
}
