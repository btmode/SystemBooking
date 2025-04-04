using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBroni.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNumberOnNameInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tables");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
