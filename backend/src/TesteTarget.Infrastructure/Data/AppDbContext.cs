using Microsoft.EntityFrameworkCore;
using TesteTarget.Domain.Comissoes;
using TesteTarget.Domain.Estoque;

namespace TesteTarget.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Movimentacao> Movimentacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venda>(entidade =>
            {
                entidade.ToTable("Venda");
                entidade.HasKey(venda => venda.Id);
                entidade.Property(venda => venda.Vendedor).HasMaxLength(200).IsRequired();
                entidade.Property(venda => venda.Valor).HasPrecision(18, 2);

                // Seed: vendas do enunciado.
                entidade.HasData(VendasIniciais());
            });

            modelBuilder.Entity<Produto>(entidade =>
            {
                entidade.ToTable("Produto");
                entidade.HasKey(produto => produto.Codigo);
                entidade.Property(produto => produto.Codigo).ValueGeneratedNever();
                entidade.Property(produto => produto.Descricao).HasMaxLength(200).IsRequired();
                entidade.Property(produto => produto.RowVersion).IsRowVersion();

                // Seed: produtos do enunciado.
                entidade.HasData(
                    new Produto { Codigo = 101, Descricao = "Caneta Azul", Estoque = 150 },
                    new Produto { Codigo = 102, Descricao = "Caderno Universitário", Estoque = 75 },
                    new Produto { Codigo = 103, Descricao = "Borracha Branca", Estoque = 200 },
                    new Produto { Codigo = 104, Descricao = "Lápis Preto HB", Estoque = 320 },
                    new Produto { Codigo = 105, Descricao = "Marcador de Texto Amarelo", Estoque = 90 });
            });

            modelBuilder.Entity<Movimentacao>(entidade =>
            {
                entidade.ToTable("Movimentacao");
                entidade.HasKey(movimentacao => movimentacao.Id);
                entidade.Property(movimentacao => movimentacao.Descricao).HasMaxLength(200).IsRequired();

                // O SQL Server devolve a data sem fuso; marcar como UTC faz o JSON sair com "Z"
                // e o navegador converter para o horário local.
                entidade.Property(movimentacao => movimentacao.DataHora).HasConversion(
                    valorParaGravar => valorParaGravar,
                    valorLido => DateTime.SpecifyKind(valorLido, DateTimeKind.Utc));

                entidade.Property(movimentacao => movimentacao.Tipo).HasConversion<string>().HasMaxLength(10);

                entidade.HasOne(movimentacao => movimentacao.Produto)
                        .WithMany()
                        .HasForeignKey(movimentacao => movimentacao.CodigoProduto);

                entidade.HasIndex(movimentacao => movimentacao.CodigoProduto);
            });
        }

        private static List<Venda> VendasIniciais()
        {
            var vendas = new List<Venda>();

            AdicionarVenda(vendas, "João Silva", 1200.50m);
            AdicionarVenda(vendas, "João Silva", 950.75m);
            AdicionarVenda(vendas, "João Silva", 1800.00m);
            AdicionarVenda(vendas, "João Silva", 1400.30m);
            AdicionarVenda(vendas, "João Silva", 1100.90m);
            AdicionarVenda(vendas, "João Silva", 1550.00m);
            AdicionarVenda(vendas, "João Silva", 1700.80m);
            AdicionarVenda(vendas, "João Silva", 250.30m);
            AdicionarVenda(vendas, "João Silva", 480.75m);
            AdicionarVenda(vendas, "João Silva", 320.40m);

            AdicionarVenda(vendas, "Maria Souza", 2100.40m);
            AdicionarVenda(vendas, "Maria Souza", 1350.60m);
            AdicionarVenda(vendas, "Maria Souza", 950.20m);
            AdicionarVenda(vendas, "Maria Souza", 1600.75m);
            AdicionarVenda(vendas, "Maria Souza", 1750.00m);
            AdicionarVenda(vendas, "Maria Souza", 1450.90m);
            AdicionarVenda(vendas, "Maria Souza", 400.50m);
            AdicionarVenda(vendas, "Maria Souza", 180.20m);
            AdicionarVenda(vendas, "Maria Souza", 90.75m);

            AdicionarVenda(vendas, "Carlos Oliveira", 800.50m);
            AdicionarVenda(vendas, "Carlos Oliveira", 1200.00m);
            AdicionarVenda(vendas, "Carlos Oliveira", 1950.30m);
            AdicionarVenda(vendas, "Carlos Oliveira", 1750.80m);
            AdicionarVenda(vendas, "Carlos Oliveira", 1300.60m);
            AdicionarVenda(vendas, "Carlos Oliveira", 300.40m);
            AdicionarVenda(vendas, "Carlos Oliveira", 500.00m);
            AdicionarVenda(vendas, "Carlos Oliveira", 125.75m);

            AdicionarVenda(vendas, "Ana Lima", 1000.00m);
            AdicionarVenda(vendas, "Ana Lima", 1100.50m);
            AdicionarVenda(vendas, "Ana Lima", 1250.75m);
            AdicionarVenda(vendas, "Ana Lima", 1400.20m);
            AdicionarVenda(vendas, "Ana Lima", 1550.90m);
            AdicionarVenda(vendas, "Ana Lima", 1650.00m);
            AdicionarVenda(vendas, "Ana Lima", 75.30m);
            AdicionarVenda(vendas, "Ana Lima", 420.90m);
            AdicionarVenda(vendas, "Ana Lima", 315.40m);

            return vendas;
        }

        private static void AdicionarVenda(List<Venda> vendas, string vendedor, decimal valor)
        {
            var venda = new Venda();
            venda.Id = vendas.Count + 1; // o HasData exige chave explícita
            venda.Vendedor = vendedor;
            venda.Valor = valor;

            vendas.Add(venda);
        }
    }
}
