using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBroni.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStatusAndAddEndTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VipRooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "BookingTime",
                table: "VipRoomBookings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "BookingTime",
                table: "TableBookings",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "VipRoomBookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TableBookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "VipRoomBookings");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TableBookings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "VipRoomBookings",
                newName: "BookingTime");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "TableBookings",
                newName: "BookingTime");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VipRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
