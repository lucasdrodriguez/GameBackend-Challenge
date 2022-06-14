using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darewise_Challenge.Migrations
{
    public partial class newPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Players");
        }
    }
}
