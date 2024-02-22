using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Quotations_QuotationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_QuotationId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "Rooms");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Quotations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProductQuotation",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuotation", x => new { x.ProductId, x.QuotationId });
                    table.ForeignKey(
                        name: "FK_ProductQuotation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuotation_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_RoomId",
                table: "Quotations",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuotation_QuotationId",
                table: "ProductQuotation",
                column: "QuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_Rooms_RoomId",
                table: "Quotations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_Rooms_RoomId",
                table: "Quotations");

            migrationBuilder.DropTable(
                name: "ProductQuotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_RoomId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Quotations");

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_QuotationId",
                table: "Rooms",
                column: "QuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Quotations_QuotationId",
                table: "Rooms",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
