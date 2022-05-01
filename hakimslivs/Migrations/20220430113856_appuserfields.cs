using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class appuserfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Identity",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                schema: "Identity",
                table: "User",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                schema: "Identity",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                schema: "Identity",
                table: "User",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Street",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                schema: "Identity",
                table: "User");
        }
    }
}
