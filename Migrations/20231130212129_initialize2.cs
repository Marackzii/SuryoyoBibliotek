using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuryoyoBibliotek.Migrations
{
    /// <inheritdoc />
    public partial class initialize2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserID",
                table: "Books",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserID",
                table: "Books",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Books");
        }
    }
}
