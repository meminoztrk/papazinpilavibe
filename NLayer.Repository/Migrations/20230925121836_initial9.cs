using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessComment_Business_BusinessId",
                table: "BusinessComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFAQ_Business_BusinessId",
                table: "BusinessFAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUserImage_BusinessComment_BusinessCommentId",
                table: "BusinessUserImage");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUserImage_Users_UserId",
                table: "BusinessUserImage");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BusinessUserImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessCommentId",
                table: "BusinessUserImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "BusinessUserImage",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "BusinessFAQ",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "BusinessComment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BusinessComment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessSubComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BusinessId = table.Column<int>(type: "int", nullable: true),
                    BusinessCommentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSubComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessSubComment_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessSubComment_BusinessComment_BusinessCommentId",
                        column: x => x.BusinessCommentId,
                        principalTable: "BusinessComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessSubComment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUserImage_BusinessId",
                table: "BusinessUserImage",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessComment_UserId",
                table: "BusinessComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubComment_BusinessCommentId",
                table: "BusinessSubComment",
                column: "BusinessCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubComment_BusinessId",
                table: "BusinessSubComment",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubComment_UserId",
                table: "BusinessSubComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessComment_Business_BusinessId",
                table: "BusinessComment",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessComment_Users_UserId",
                table: "BusinessComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFAQ_Business_BusinessId",
                table: "BusinessFAQ",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUserImage_Business_BusinessId",
                table: "BusinessUserImage",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUserImage_BusinessComment_BusinessCommentId",
                table: "BusinessUserImage",
                column: "BusinessCommentId",
                principalTable: "BusinessComment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUserImage_Users_UserId",
                table: "BusinessUserImage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessComment_Business_BusinessId",
                table: "BusinessComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessComment_Users_UserId",
                table: "BusinessComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFAQ_Business_BusinessId",
                table: "BusinessFAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUserImage_Business_BusinessId",
                table: "BusinessUserImage");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUserImage_BusinessComment_BusinessCommentId",
                table: "BusinessUserImage");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessUserImage_Users_UserId",
                table: "BusinessUserImage");

            migrationBuilder.DropTable(
                name: "BusinessSubComment");

            migrationBuilder.DropIndex(
                name: "IX_BusinessUserImage_BusinessId",
                table: "BusinessUserImage");

            migrationBuilder.DropIndex(
                name: "IX_BusinessComment_UserId",
                table: "BusinessComment");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "BusinessUserImage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BusinessComment");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BusinessUserImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessCommentId",
                table: "BusinessUserImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "BusinessFAQ",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "BusinessComment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessComment_Business_BusinessId",
                table: "BusinessComment",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFAQ_Business_BusinessId",
                table: "BusinessFAQ",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUserImage_BusinessComment_BusinessCommentId",
                table: "BusinessUserImage",
                column: "BusinessCommentId",
                principalTable: "BusinessComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessUserImage_Users_UserId",
                table: "BusinessUserImage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
