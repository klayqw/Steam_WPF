using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class up8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Workshop_WorkShopId",
                table: "Content");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropIndex(
                name: "IX_Content_WorkShopId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "WorkShopId",
                table: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkShopId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workshop_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_WorkShopId",
                table: "Content",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_UserId",
                table: "Workshop",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Workshop_WorkShopId",
                table: "Content",
                column: "WorkShopId",
                principalTable: "Workshop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
