using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixQuotationandRoomProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Quotations_QuotationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_QuotationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "Products");

            migrationBuilder.AddColumn<float>(
                name: "ActualPrice",
                table: "RoomProduct",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RoomProduct",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "RoomProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RoomProduct");

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_QuotationId",
                table: "Products",
                column: "QuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Quotations_QuotationId",
                table: "Products",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id");
        }
    }
}
