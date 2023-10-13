using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardingService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boardings",
                columns: table => new
                {
                    FlightNr = table.Column<string>(type: "text", nullable: false),
                    Gate = table.Column<int>(type: "integer", nullable: false),
                    From = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boardings", x => x.FlightNr);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<string>(type: "text", nullable: false),
                    CheckinNr = table.Column<string>(type: "text", nullable: false),
                    FlightNr = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => new { x.PassengerId, x.CheckinNr });
                    table.ForeignKey(
                        name: "FK_Passengers_Boardings_FlightNr",
                        column: x => x.FlightNr,
                        principalTable: "Boardings",
                        principalColumn: "FlightNr",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightNr",
                table: "Passengers",
                column: "FlightNr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Boardings");
        }
    }
}
