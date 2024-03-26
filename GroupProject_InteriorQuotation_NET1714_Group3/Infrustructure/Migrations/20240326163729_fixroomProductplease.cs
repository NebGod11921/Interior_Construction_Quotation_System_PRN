using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixroomProductplease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_Users_UserId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "RoomProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomProduct");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomProduct");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Quotations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_Users_UserId",
                table: "Quotations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_Users_UserId",
                table: "Quotations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "RoomProduct",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomProduct",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Quotations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_Users_UserId",
                table: "Quotations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
