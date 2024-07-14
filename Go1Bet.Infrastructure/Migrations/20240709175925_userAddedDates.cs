using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Go1Bet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userAddedDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastEmailUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastPasswordUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastPersonalInfoUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateLastEmailUpdated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateLastPasswordUpdated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateLastPersonalInfoUpdated",
                table: "AspNetUsers");
        }
    }
}
