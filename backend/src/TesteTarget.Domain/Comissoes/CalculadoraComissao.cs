namespace TesteTarget.Domain.Comissoes
{
    public static class CalculadoraComissao
    {
        private const decimal LimiteSemComissao = 100m;
        private const decimal LimiteComissaoReduzida = 500m;
        private const decimal TaxaReduzida = 0.01m;
        private const decimal TaxaPadrao = 0.05m;

        // Abaixo de 100: 0% | de 100 a 499,99: 1% | a partir de 500: 5%
        // Retorna o valor exato; o arredondamento é feito uma única vez, no total do vendedor.
        public static decimal CalcularPorVenda(decimal valor)
        {
            decimal taxa;

            if (valor < LimiteSemComissao)
            {
                taxa = 0m;
            }
            else if (valor < LimiteComissaoReduzida)
            {
                taxa = TaxaReduzida;
            }
            else
            {
                taxa = TaxaPadrao;
            }

            return valor * taxa;
        }

        public static List<ResumoVendedor> CalcularPorVendedor(IEnumerable<Venda> vendas)
        {
            var resumos = new List<ResumoVendedor>();
            var grupos = vendas.GroupBy(venda => venda.Vendedor);

            foreach (var grupo in grupos)
            {
                var resumo = new ResumoVendedor();
                resumo.Vendedor = grupo.Key;

                foreach (var venda in grupo)
                {
                    resumo.QuantidadeVendas++;
                    resumo.TotalVendido += venda.Valor;
                    resumo.TotalComissao += CalcularPorVenda(venda.Valor);
                }

                resumo.TotalComissao = Math.Round(resumo.TotalComissao, 2, MidpointRounding.AwayFromZero);
                resumos.Add(resumo);
            }

            return resumos;
        }
    }
}
