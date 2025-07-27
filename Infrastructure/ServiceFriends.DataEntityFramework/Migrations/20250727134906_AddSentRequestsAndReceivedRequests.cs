using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFriends.DataEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddSentRequestsAndReceivedRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Friends");

            migrationBuilder.CreateTable(
                name: "ReceivedFriendRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedFriendRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentFriendRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentFriendRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivedFriendRequests");

            migrationBuilder.DropTable(
                name: "SentFriendRequests");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Friends",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
