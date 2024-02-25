using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRifa.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexRaffleInDraw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Draws_RaffleId",
                table: "Draws");

            migrationBuilder.CreateIndex(
                name: "IX_Draws_RaffleId_IsDeleted",
                table: "Draws",
                columns: new[] { "RaffleId", "IsDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Draws_RaffleId_IsDeleted",
                table: "Draws");

            migrationBuilder.CreateIndex(
                name: "IX_Draws_RaffleId",
                table: "Draws",
                column: "RaffleId");
        }
    }
}
