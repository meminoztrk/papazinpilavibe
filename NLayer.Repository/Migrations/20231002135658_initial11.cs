using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MergedArea",
                table: "Provinces",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MergedArea",
                table: "Provinces");
        }
    }
}
