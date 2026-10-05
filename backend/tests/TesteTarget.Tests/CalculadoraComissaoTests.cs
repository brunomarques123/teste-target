using TesteTarget.Domain.Comissoes;

namespace TesteTarget.Tests
{
    public class CalculadoraComissaoTests
    {
        [Theory]
        [InlineData(90.75, 0)]
        [InlineData(99.99, 0)]
        [InlineData(100.00, 1.00)]
        [InlineData(499.99, 4.9999)]
        [InlineData(500.00, 25.00)]
        [InlineData(1200.50, 60.025)]
        public void CalcularPorVenda_AplicaFaixaCorreta(double valor, double esperado)
        {
            var resultado = CalculadoraComissao.CalcularPorVenda((decimal)valor);

            Assert.Equal((decimal)esperado, resultado);
        }

        [Fact]
        public void CalcularPorVendedor_AgrupaESomaPorVendedor()
        {
            var vendas = new List<Venda>();
            vendas.Add(new Venda { Vendedor = "Ana", Valor = 1000m });
            vendas.Add(new Venda { Vendedor = "Ana", Valor = 200m });
            vendas.Add(new Venda { Vendedor = "Beto", Valor = 50m });

            var resumos = CalculadoraComissao.CalcularPorVendedor(vendas);

            var ana = resumos.Single(resumo => resumo.Vendedor == "Ana");
            var beto = resumos.Single(resumo => resumo.Vendedor == "Beto");

            Assert.Equal(1200m, ana.TotalVendido);
            Assert.Equal(52m, ana.TotalComissao);
            Assert.Equal(0m, beto.TotalComissao);
        }

        // Arredondar cada venda daria 495,69; o correto é arredondar só o total (495,677).
        [Fact]
        public void CalcularPorVendedor_ArredondaApenasONoTotal()
        {
            decimal[] valores = { 1200.50m, 950.75m, 1800.00m, 1400.30m, 1100.90m, 1550.00m, 1700.80m, 250.30m, 480.75m, 320.40m };

            var vendas = new List<Venda>();

            foreach (var valor in valores)
            {
                vendas.Add(new Venda { Vendedor = "João Silva", Valor = valor });
            }

            var resumos = CalculadoraComissao.CalcularPorVendedor(vendas);

            Assert.Equal(495.68m, resumos.Single().TotalComissao);
        }
    }
}
