using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBroni.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableBookings_Users_UserId1",
                table: "TableBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_VipRoomBookings_Users_UserId1",
                table: "VipRoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_VipRoomBookings_UserId1",
                table: "VipRoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_TableBookings_UserId1",
                table: "TableBookings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "VipRoomBookings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TableBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "VipRoomBookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "TableBookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VipRoomBookings_UserId1",
                table: "VipRoomBookings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_UserId1",
                table: "TableBookings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TableBookings_Users_UserId1",
                table: "TableBookings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VipRoomBookings_Users_UserId1",
                table: "VipRoomBookings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
