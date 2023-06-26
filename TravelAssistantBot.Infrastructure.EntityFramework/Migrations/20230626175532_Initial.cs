using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAssistantBot.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IATA = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arrivals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Airport = table.Column<string>(type: "TEXT", nullable: false),
                    IATA = table.Column<string>(type: "TEXT", nullable: false),
                    Terminal = table.Column<string>(type: "TEXT", nullable: true),
                    Gate = table.Column<string>(type: "TEXT", nullable: true),
                    Delay = table.Column<string>(type: "TEXT", nullable: true),
                    Scheduled = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estimated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Actual = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrivals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Airport = table.Column<string>(type: "TEXT", nullable: false),
                    IATA = table.Column<string>(type: "TEXT", nullable: false),
                    Terminal = table.Column<string>(type: "TEXT", nullable: true),
                    Gate = table.Column<string>(type: "TEXT", nullable: true),
                    Delay = table.Column<string>(type: "TEXT", nullable: true),
                    Scheduled = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estimated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Actual = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    IATA = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FlightStatus = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArrivalId = table.Column<int>(type: "INTEGER", nullable: false),
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false),
                    FlightInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Aircraft = table.Column<string>(type: "TEXT", nullable: true),
                    Live = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Arrivals_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Arrivals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_FlightInfos_FlightInfoId",
                        column: x => x.FlightInfoId,
                        principalTable: "FlightInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "Id", "FlightId", "IATA", "Name" },
                values: new object[,]
                {
                    { 1, 1, "AA", "American Airlines" },
                    { 2, 2, "5D", "AeroMexico Connect" },
                    { 3, 3, "CM", "Copa Airlines" },
                    { 4, 4, "AV", "SA AVIANCA" },
                    { 5, 5, "NK", "Spirit Airlines" },
                    { 6, 6, "AA", "American Airlines" },
                    { 7, 7, "AF", "Air France" },
                    { 8, 8, "QF", "Qantas" },
                    { 9, 9, "AA", "American Airlines" },
                    { 10, 10, "DL", "Delta Air Lines" },
                    { 11, 11, "BA", "British Airways" }
                });

            migrationBuilder.InsertData(
                table: "Arrivals",
                columns: new[] { "Id", "Actual", "Airport", "Delay", "Estimated", "FlightId", "Gate", "IATA", "Scheduled", "Terminal" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(8982), "Miami International Airport", "43", new DateTime(2023, 6, 27, 11, 38, 0, 0, DateTimeKind.Local), 1, "D6", "MIA", new DateTime(2023, 6, 27, 11, 38, 0, 0, DateTimeKind.Local), "N" },
                    { 2, new DateTime(2023, 6, 23, 8, 10, 0, 0, DateTimeKind.Local), "Internacional Benito Juarez", "", new DateTime(2023, 6, 27, 8, 23, 0, 0, DateTimeKind.Local), 2, "P", "MEX", new DateTime(2023, 6, 27, 8, 23, 0, 0, DateTimeKind.Local), "2" },
                    { 3, new DateTime(2023, 6, 23, 11, 10, 0, 0, DateTimeKind.Local), "Tocumen International", "8", new DateTime(2023, 6, 27, 11, 3, 0, 0, DateTimeKind.Local), 3, "103", "PTY", new DateTime(2023, 6, 27, 11, 3, 0, 0, DateTimeKind.Local), "1" },
                    { 4, new DateTime(2023, 6, 23, 10, 52, 0, 0, DateTimeKind.Local), "El Salvador International", "", new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Local), 4, "", "SAL", new DateTime(2023, 6, 28, 11, 0, 0, 0, DateTimeKind.Local), "E" },
                    { 5, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9154), "Logan International", "", new DateTime(2023, 6, 28, 3, 38, 0, 0, DateTimeKind.Local), 5, "", "BOS", new DateTime(2023, 6, 28, 3, 38, 0, 0, DateTimeKind.Local), "B" },
                    { 6, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9232), "John F Kennedy International", "", new DateTime(2023, 6, 29, 4, 52, 0, 0, DateTimeKind.Local), 6, "1", "JFK", new DateTime(2023, 6, 29, 4, 52, 0, 0, DateTimeKind.Local), "8" },
                    { 7, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9267), "Charles De Gaulle", "", new DateTime(2023, 6, 29, 8, 15, 0, 0, DateTimeKind.Local), 7, "", "CDG", new DateTime(2023, 6, 29, 8, 15, 0, 0, DateTimeKind.Local), "2E" },
                    { 8, new DateTime(2023, 6, 25, 5, 15, 0, 0, DateTimeKind.Local), "Los Angeles International", "", new DateTime(2023, 6, 30, 5, 40, 0, 0, DateTimeKind.Local), 8, "53B", "LAX", new DateTime(2023, 6, 30, 5, 40, 0, 0, DateTimeKind.Local), "5" },
                    { 9, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9330), "Juan Santamaría International", "78", new DateTime(2023, 7, 1, 13, 50, 0, 0, DateTimeKind.Local), 9, "A4A", "SJO", new DateTime(2023, 7, 1, 13, 50, 0, 0, DateTimeKind.Local), "1" },
                    { 10, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9364), "Barajas", "", new DateTime(2023, 7, 2, 6, 5, 0, 0, DateTimeKind.Local), 10, "", "MAD", new DateTime(2023, 7, 2, 6, 5, 0, 0, DateTimeKind.Local), "1" },
                    { 11, new DateTime(2023, 6, 25, 9, 35, 0, 0, DateTimeKind.Local), "Miami International Airport", "", new DateTime(2023, 7, 2, 9, 40, 0, 0, DateTimeKind.Local), 11, "D44", "MIA", new DateTime(2023, 7, 2, 9, 40, 0, 0, DateTimeKind.Local), "N" }
                });

            migrationBuilder.InsertData(
                table: "Departures",
                columns: new[] { "Id", "Actual", "Airport", "Delay", "Estimated", "FlightId", "Gate", "IATA", "Scheduled", "Terminal" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 23, 8, 30, 0, 0, DateTimeKind.Local), "Ramon Villeda Morales International", "76", new DateTime(2023, 6, 26, 7, 14, 0, 0, DateTimeKind.Local), 1, "", "SAP", new DateTime(2023, 6, 26, 7, 14, 0, 0, DateTimeKind.Local), "" },
                    { 2, new DateTime(2023, 6, 23, 6, 14, 0, 0, DateTimeKind.Local), "Ramon Villeda Morales International", "15", new DateTime(2023, 6, 26, 6, 0, 0, 0, DateTimeKind.Local), 2, "", "SAP", new DateTime(2023, 6, 26, 6, 0, 0, 0, DateTimeKind.Local), "" },
                    { 3, new DateTime(2023, 6, 23, 8, 26, 0, 0, DateTimeKind.Local), "Ramon Villeda Morales International", "36", new DateTime(2023, 6, 27, 7, 51, 0, 0, DateTimeKind.Local), 3, "5", "SAP", new DateTime(2023, 6, 27, 7, 51, 0, 0, DateTimeKind.Local), "" },
                    { 4, new DateTime(2023, 6, 23, 10, 23, 0, 0, DateTimeKind.Local), "Ramon Villeda Morales International", "8", new DateTime(2023, 6, 27, 10, 15, 0, 0, DateTimeKind.Local), 4, "", "SAP", new DateTime(2023, 6, 27, 10, 15, 0, 0, DateTimeKind.Local), "" },
                    { 5, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9144), "Miami International Airport", "", new DateTime(2023, 6, 27, 0, 10, 0, 0, DateTimeKind.Local), 5, "G12", "MIA", new DateTime(2023, 6, 27, 0, 10, 0, 0, DateTimeKind.Local), "C" },
                    { 6, new DateTime(2023, 6, 26, 11, 55, 32, 488, DateTimeKind.Local).AddTicks(9222), "Miami International Airport", "", new DateTime(2023, 6, 28, 1, 50, 0, 0, DateTimeKind.Local), 6, "D32", "MIA", new DateTime(2023, 6, 28, 1, 50, 0, 0, DateTimeKind.Local), "N" },
                    { 7, new DateTime(2023, 6, 23, 17, 52, 0, 0, DateTimeKind.Local), "Miami International Airport", "33", new DateTime(2023, 6, 29, 17, 20, 0, 0, DateTimeKind.Local), 7, "J18", "MIA", new DateTime(2023, 6, 29, 17, 20, 0, 0, DateTimeKind.Local), "S" },
                    { 8, new DateTime(2023, 6, 25, 3, 22, 0, 0, DateTimeKind.Local), "Miami International Airport", "22", new DateTime(2023, 6, 30, 3, 1, 0, 0, DateTimeKind.Local), 8, "E5", "MIA", new DateTime(2023, 6, 30, 3, 1, 0, 0, DateTimeKind.Local), "C" },
                    { 9, new DateTime(2023, 6, 25, 14, 44, 0, 0, DateTimeKind.Local), "Miami International Airport", "124", new DateTime(2023, 6, 30, 12, 52, 0, 0, DateTimeKind.Local), 9, "D42", "MIA", new DateTime(2023, 6, 30, 12, 52, 0, 0, DateTimeKind.Local), "N" },
                    { 10, new DateTime(2023, 6, 25, 15, 57, 0, 0, DateTimeKind.Local), "Miami International Airport", "27", new DateTime(2023, 7, 1, 15, 30, 0, 0, DateTimeKind.Local), 10, "E31", "MIA", new DateTime(2023, 7, 1, 15, 30, 0, 0, DateTimeKind.Local), "C" },
                    { 11, new DateTime(2023, 6, 25, 4, 40, 0, 0, DateTimeKind.Local), "Internacional Benito Juarez", "20", new DateTime(2023, 7, 2, 4, 20, 0, 0, DateTimeKind.Local), 11, "33", "MEX", new DateTime(2023, 7, 2, 4, 20, 0, 0, DateTimeKind.Local), "1" }
                });

            migrationBuilder.InsertData(
                table: "FlightInfos",
                columns: new[] { "Id", "FlightId", "IATA", "Number" },
                values: new object[,]
                {
                    { 1, 1, "AA1312", "1312" },
                    { 2, 2, "5D675", "675" },
                    { 3, 3, "CM287", "287" },
                    { 4, 4, "AV537", "537" },
                    { 5, 5, "NK3121", "3121" },
                    { 6, 6, "AA315", "315" },
                    { 7, 7, "AF97", "97" },
                    { 8, 8, "QF3130", "3130" },
                    { 9, 9, "AA1353", "1353" },
                    { 10, 10, "DL6774", "6774" },
                    { 11, 11, "BA4884", "4884" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Aircraft", "AirlineId", "ArrivalId", "DepartureId", "FlightDate", "FlightInfoId", "FlightStatus", "Live" },
                values: new object[,]
                {
                    { 1, "", 1, 1, 1, new DateTime(2023, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "scheduled", "" },
                    { 2, "", 2, 2, 2, new DateTime(2023, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "landed", "" },
                    { 3, "", 3, 3, 3, new DateTime(2023, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "landed", "" },
                    { 4, "", 4, 4, 4, new DateTime(2023, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "landed", "" },
                    { 5, "", 5, 5, 5, new DateTime(2023, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "scheduled", "" },
                    { 6, "", 6, 6, 6, new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "scheduled", "" },
                    { 7, "", 7, 7, 7, new DateTime(2023, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "active", "" },
                    { 8, "", 8, 8, 8, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "landed", "" },
                    { 9, "", 9, 9, 9, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "scheduled", "" },
                    { 10, "", 10, 10, 10, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "active", "" },
                    { 11, "", 11, 11, 11, new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "landed", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureId",
                table: "Flights",
                column: "DepartureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightInfoId",
                table: "Flights",
                column: "FlightInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Arrivals");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "FlightInfos");
        }
    }
}
