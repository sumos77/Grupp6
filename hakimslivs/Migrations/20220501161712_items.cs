using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemQuantities_UserTokens_OrderID",
                schema: "Identity",
                table: "ItemQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_IdentityUser_AspNetUsersId",
                schema: "Identity",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "IdentityUser",
                schema: "Identity");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserTokens_TempId",
                schema: "Identity",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                schema: "Identity",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ID",
                schema: "Identity",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "TempId",
                schema: "Identity",
                table: "UserTokens");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    AspNetUsersId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_User_AspNetUsersId",
                        column: x => x.AspNetUsersId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AspNetUsersId",
                schema: "Identity",
                table: "Orders",
                column: "AspNetUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemQuantities_Orders_OrderID",
                schema: "Identity",
                table: "ItemQuantities",
                column: "OrderID",
                principalSchema: "Identity",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemQuantities_Orders_OrderID",
                schema: "Identity",
                table: "ItemQuantities");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Identity");

            migrationBuilder.AddColumn<int>(
                name: "AspNetUsersId",
                schema: "Identity",
                table: "UserTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                schema: "Identity",
                table: "UserTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                schema: "Identity",
                table: "UserTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserTokens_TempId",
                schema: "Identity",
                table: "UserTokens",
                column: "TempId");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                schema: "Identity",
                columns: table => new
                {
                    TempId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.UniqueConstraint("AK_IdentityUser_TempId", x => x.TempId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemQuantities_UserTokens_OrderID",
                schema: "Identity",
                table: "ItemQuantities",
                column: "OrderID",
                principalSchema: "Identity",
                principalTable: "UserTokens",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_IdentityUser_AspNetUsersId",
                schema: "Identity",
                table: "UserTokens",
                column: "AspNetUsersId",
                principalSchema: "Identity",
                principalTable: "IdentityUser",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
