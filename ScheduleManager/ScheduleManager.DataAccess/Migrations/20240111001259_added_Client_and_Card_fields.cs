using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleManager.DataAccess.Migrations
{
    public partial class added_Client_and_Card_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Card",
                table: "TripRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "TripRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Card",
                table: "TripRequests");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "TripRequests");
        }
    }
}
