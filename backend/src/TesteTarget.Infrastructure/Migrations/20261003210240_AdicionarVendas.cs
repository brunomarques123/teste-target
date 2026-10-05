using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TesteTarget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarVendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendedor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Venda",
                columns: new[] { "Id", "Valor", "Vendedor" },
                values: new object[,]
                {
                    { 1, 1200.50m, "João Silva" },
                    { 2, 950.75m, "João Silva" },
                    { 3, 1800.00m, "João Silva" },
                    { 4, 1400.30m, "João Silva" },
                    { 5, 1100.90m, "João Silva" },
                    { 6, 1550.00m, "João Silva" },
                    { 7, 1700.80m, "João Silva" },
                    { 8, 250.30m, "João Silva" },
                    { 9, 480.75m, "João Silva" },
                    { 10, 320.40m, "João Silva" },
                    { 11, 2100.40m, "Maria Souza" },
                    { 12, 1350.60m, "Maria Souza" },
                    { 13, 950.20m, "Maria Souza" },
                    { 14, 1600.75m, "Maria Souza" },
                    { 15, 1750.00m, "Maria Souza" },
                    { 16, 1450.90m, "Maria Souza" },
                    { 17, 400.50m, "Maria Souza" },
                    { 18, 180.20m, "Maria Souza" },
                    { 19, 90.75m, "Maria Souza" },
                    { 20, 800.50m, "Carlos Oliveira" },
                    { 21, 1200.00m, "Carlos Oliveira" },
                    { 22, 1950.30m, "Carlos Oliveira" },
                    { 23, 1750.80m, "Carlos Oliveira" },
                    { 24, 1300.60m, "Carlos Oliveira" },
                    { 25, 300.40m, "Carlos Oliveira" },
                    { 26, 500.00m, "Carlos Oliveira" },
                    { 27, 125.75m, "Carlos Oliveira" },
                    { 28, 1000.00m, "Ana Lima" },
                    { 29, 1100.50m, "Ana Lima" },
                    { 30, 1250.75m, "Ana Lima" },
                    { 31, 1400.20m, "Ana Lima" },
                    { 32, 1550.90m, "Ana Lima" },
                    { 33, 1650.00m, "Ana Lima" },
                    { 34, 75.30m, "Ana Lima" },
                    { 35, 420.90m, "Ana Lima" },
                    { 36, 315.40m, "Ana Lima" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venda");
        }
    }
}
