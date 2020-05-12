using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell.Data.Migrations
{
    public partial class migpic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateOn = table.Column<DateTime>(nullable: false),
                    UpdateOn = table.Column<DateTime>(nullable: false),
                    VirtualPath = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Picture_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateOn", "UpdateOn" },
                values: new object[] { new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ProductID",
                table: "Picture",
                column: "ProductID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateOn", "UpdateOn" },
                values: new object[] { new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
