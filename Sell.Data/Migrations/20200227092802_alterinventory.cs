using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell.Data.Migrations
{
    public partial class alterinventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InventoryDesc",
                table: "Inventory",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateOn", "UpdateOn" },
                values: new object[] { new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InventoryDesc",
                table: "Inventory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateOn", "UpdateOn" },
                values: new object[] { new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
