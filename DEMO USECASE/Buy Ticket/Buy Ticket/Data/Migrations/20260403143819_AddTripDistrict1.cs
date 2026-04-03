using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buy_Ticket.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTripDistrict1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AvailableSeats", "DateText", "DepartureTime", "DestinationId", "Price" },
                values: new object[] { 7, 40, "April 4, 2026", "10:00 AM", 1, 200000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
