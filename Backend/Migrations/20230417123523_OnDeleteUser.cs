using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Users_UserId",
                table: "Labs");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Users_UserId",
                table: "Labs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Users_UserId",
                table: "Labs");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Users_UserId",
                table: "Labs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
