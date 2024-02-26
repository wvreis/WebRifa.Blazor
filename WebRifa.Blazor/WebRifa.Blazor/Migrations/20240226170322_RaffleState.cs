using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRifa.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class RaffleState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "Raffles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Raffles");
        }
    }
}
