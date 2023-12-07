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
            migrationBuilder.RenameColumn(
                name: "Loaned",
                table: "Books",
                newName: "Borrowed");

            migrationBuilder.RenameColumn(
                name: "LoanDate",
                table: "Books",
                newName: "HireDate");

            migrationBuilder.RenameColumn(
                name: "LoanCardId",
                table: "Books",
                newName: "RentCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RentCardId",
                table: "Books",
                newName: "LoanCardId");

            migrationBuilder.RenameColumn(
                name: "HireDate",
                table: "Books",
                newName: "LoanDate");

            migrationBuilder.RenameColumn(
                name: "Borrowed",
                table: "Books",
                newName: "Loaned");
        }
    }
}
