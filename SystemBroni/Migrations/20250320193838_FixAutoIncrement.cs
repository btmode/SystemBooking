using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SystemBroni.Migrations
{
    /// <inheritdoc />
    public partial class FixAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "VipRooms");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Tables",
                newName: "Capacity");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VipRooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TableBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableBookings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TableBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableBookings_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VipRoomBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VipRoomId = table.Column<int>(type: "integer", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VipRoomBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VipRoomBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VipRoomBookings_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VipRoomBookings_VipRooms_VipRoomId",
                        column: x => x.VipRoomId,
                        principalTable: "VipRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_TableId",
                table: "TableBookings",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_UserId",
                table: "TableBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_UserId1",
                table: "TableBookings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_VipRoomBookings_UserId",
                table: "VipRoomBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VipRoomBookings_UserId1",
                table: "VipRoomBookings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_VipRoomBookings_VipRoomId",
                table: "VipRoomBookings",
                column: "VipRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableBookings");

            migrationBuilder.DropTable(
                name: "VipRoomBookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VipRooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Tables",
                newName: "Seats");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "VipRooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Tables",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VipRoomId = table.Column<int>(type: "integer", nullable: true),
                    BookingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_VipRooms_VipRoomId",
                        column: x => x.VipRoomId,
                        principalTable: "VipRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId",
                table: "Bookings",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VipRoomId",
                table: "Bookings",
                column: "VipRoomId");
        }
    }
}
