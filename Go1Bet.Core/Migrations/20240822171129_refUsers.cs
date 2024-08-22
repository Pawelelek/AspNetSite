using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Go1Bet.Core.Migrations
{
    /// <inheritdoc />
    public partial class refUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bonuses_BonusesId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.RenameColumn(
                name: "BonusesId",
                table: "AspNetUsers",
                newName: "RefUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_BonusesId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RefUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RefUserId",
                table: "AspNetUsers",
                column: "RefUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RefUserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RefUserId",
                table: "AspNetUsers",
                newName: "BonusesId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RefUserId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_BonusesId");

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
                        name: "FK_Bonuses_UserExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "UserExercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bonuses_UserPromocodes_PromocodeId",
                        column: x => x.PromocodeId,
                        principalTable: "UserPromocodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_ExerciseId",
                table: "Bonuses",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_PromocodeId",
                table: "Bonuses",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bonuses_BonusesId",
                table: "AspNetUsers",
                column: "BonusesId",
                principalTable: "Bonuses",
                principalColumn: "Id");
        }
    }
}
