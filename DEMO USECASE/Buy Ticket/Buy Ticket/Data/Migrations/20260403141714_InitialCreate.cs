using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Buy_Ticket.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PaymentRef = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DestinationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DepartureTime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DateText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QrPayload = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    DateText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DepartureTime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Cultural and financial heart of the city", "⭐", "District 1" },
                    { 2, "Domestic & International Terminals", "✈️", "Tan Son Nhat Airport" },
                    { 3, "District 3, Central Hub", "🚆", "Saigon Railway Station" },
                    { 4, "East City Terminal Connections", "🚌", "Da Lat Bus Station" },
                    { 5, "Emerging District 2 Riverside", "🏙️", "Thu Thiem New Area" }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AvailableSeats", "DateText", "DepartureTime", "DestinationId", "Price" },
                values: new object[,]
                {
                    { 1, 22, "April 4, 2026", "08:30 AM", 4, 350000m },
                    { 2, 18, "April 4, 2026", "01:45 PM", 4, 350000m },
                    { 3, 12, "April 4, 2026", "08:00 PM", 4, 370000m },
                    { 4, 30, "April 4, 2026", "09:00 AM", 2, 150000m },
                    { 5, 26, "April 4, 2026", "11:20 AM", 3, 180000m },
                    { 6, 28, "April 4, 2026", "07:40 AM", 5, 120000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips",
                column: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrders");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
