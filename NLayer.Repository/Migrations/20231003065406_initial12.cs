using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class initial12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Provinces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Provinces");
        }
    }
}
