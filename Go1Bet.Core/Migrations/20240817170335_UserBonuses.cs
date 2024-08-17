using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Go1Bet.Core.Migrations
{
    /// <inheritdoc />
    public partial class UserBonuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BonusesId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserBonuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBonuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPromocodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromocodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPromocodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExerciseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PromocodeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonuses_UserBonuses_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "UserBonuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bonuses_UserPromocodes_PromocodeId",
                        column: x => x.PromocodeId,
                        principalTable: "UserPromocodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BonusesId",
                table: "AspNetUsers",
                column: "BonusesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_ExerciseId",
                table: "Bonuses",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_PromocodeId",
                table: "Bonuses",
                column: "PromocodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBonuses_UserId",
                table: "UserBonuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocodes_UserId",
                table: "UserPromocodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bonuses_BonusesId",
                table: "AspNetUsers",
                column: "BonusesId",
                principalTable: "Bonuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bonuses_BonusesId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "UserBonuses");

            migrationBuilder.DropTable(
                name: "UserPromocodes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BonusesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BonusesId",
                table: "AspNetUsers");
        }
    }
}
