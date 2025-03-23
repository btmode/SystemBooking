using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBroni.Migrations
{
    /// <inheritdoc />
    public partial class addstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VipRooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tables");
        }
    }
}
