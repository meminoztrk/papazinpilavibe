using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessProps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapIframe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    HasCourier = table.Column<bool>(type: "bit", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    We = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Th = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Su = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Business_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Business_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessFAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFAQ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFAQ_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImage_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_ProvinceId",
                table: "Business",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_UserId",
                table: "Business",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFAQ_BusinessId",
                table: "BusinessFAQ",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImage_BusinessId",
                table: "BusinessImage",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessFAQ");

            migrationBuilder.DropTable(
                name: "BusinessImage");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Critical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Explain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }
    }
}
