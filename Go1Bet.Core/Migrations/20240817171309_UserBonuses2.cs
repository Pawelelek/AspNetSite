using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Go1Bet.Core.Migrations
{
    /// <inheritdoc />
    public partial class UserBonuses2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_UserBonuses_ExerciseId",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBonuses_AspNetUsers_UserId",
                table: "UserBonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBonuses",
                table: "UserBonuses");

            migrationBuilder.RenameTable(
                name: "UserBonuses",
                newName: "BonusUserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserBonuses_UserId",
                table: "BonusUserEntity",
                newName: "IX_BonusUserEntity_UserId");

            migrationBuilder.AddColumn<string>(
                name: "PromocodeId",
                table: "UserPromocodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonusUserEntity",
                table: "BonusUserEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserExercises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExercises_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocodes_PromocodeId",
                table: "UserPromocodes",
                column: "PromocodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExercises_ExerciseId",
                table: "UserExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExercises_UserId",
                table: "UserExercises",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_BonusUserEntity_ExerciseId",
                table: "Bonuses",
                column: "ExerciseId",
                principalTable: "BonusUserEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusUserEntity_AspNetUsers_UserId",
                table: "BonusUserEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPromocodes_Promocodes_PromocodeId",
                table: "UserPromocodes",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_BonusUserEntity_ExerciseId",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusUserEntity_AspNetUsers_UserId",
                table: "BonusUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPromocodes_Promocodes_PromocodeId",
                table: "UserPromocodes");

            migrationBuilder.DropTable(
                name: "UserExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserPromocodes_PromocodeId",
                table: "UserPromocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonusUserEntity",
                table: "BonusUserEntity");

            migrationBuilder.DropColumn(
                name: "PromocodeId",
                table: "UserPromocodes");

            migrationBuilder.RenameTable(
                name: "BonusUserEntity",
                newName: "UserBonuses");

            migrationBuilder.RenameIndex(
                name: "IX_BonusUserEntity_UserId",
                table: "UserBonuses",
                newName: "IX_UserBonuses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBonuses",
                table: "UserBonuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_UserBonuses_ExerciseId",
                table: "Bonuses",
                column: "ExerciseId",
                principalTable: "UserBonuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBonuses_AspNetUsers_UserId",
                table: "UserBonuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
