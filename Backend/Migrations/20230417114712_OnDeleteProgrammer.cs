using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteProgrammer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Programmers_ProgrammerId",
                table: "Labs");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Programmers_ProgrammerId",
                table: "Labs",
                column: "ProgrammerId",
                principalTable: "Programmers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Programmers_ProgrammerId",
                table: "Labs");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Programmers_ProgrammerId",
                table: "Labs",
                column: "ProgrammerId",
                principalTable: "Programmers",
                principalColumn: "Id");
        }
    }
}
