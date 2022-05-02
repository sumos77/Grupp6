using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "banana",
                schema: "Identity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banana",
                schema: "Identity",
                table: "Orders");
        }
    }
}
