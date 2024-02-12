using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRifa.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class BuyerIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Buyers_CreatedBy",
                table: "Buyers",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buyers_CreatedBy",
                table: "Buyers");
        }
    }
}
