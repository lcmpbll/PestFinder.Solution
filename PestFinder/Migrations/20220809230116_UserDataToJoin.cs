using Microsoft.EntityFrameworkCore.Migrations;

namespace PestFinder.Migrations
{
    public partial class UserDataToJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PestLocation",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PestLocation_UserId",
                table: "PestLocation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PestLocation_AspNetUsers_UserId",
                table: "PestLocation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PestLocation_AspNetUsers_UserId",
                table: "PestLocation");

            migrationBuilder.DropIndex(
                name: "IX_PestLocation_UserId",
                table: "PestLocation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PestLocation");
        }
    }
}
