using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRifa.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class PrecisionTicketPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TicketPrice",
                table: "Raffles",
                type: "numeric(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,0)",
                oldPrecision: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TicketPrice",
                table: "Raffles",
                type: "numeric(4,0)",
                precision: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
