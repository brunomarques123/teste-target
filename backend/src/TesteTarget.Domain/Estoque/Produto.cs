namespace TesteTarget.Domain.Estoque
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Estoque { get; set; }

        // Concorrência otimista: impede que duas movimentações simultâneas sobrescrevam o saldo.
        public byte[] RowVersion { get; set; } = new byte[0];

        public Movimentacao Movimentar(TipoMovimentacao tipo, int quantidade, string descricao)
        {
            if (quantidade <= 0)
            {
                throw new RegraDeNegocioException("A quantidade deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new RegraDeNegocioException("A descrição da movimentação é obrigatória.");
            }

            if (tipo == TipoMovimentacao.Saida && quantidade > Estoque)
            {
                throw new RegraDeNegocioException(
                    $"Estoque insuficiente de '{Descricao}': saldo {Estoque}, saída solicitada {quantidade}.");
            }

            if (tipo == TipoMovimentacao.Entrada)
            {
                Estoque = Estoque + quantidade;
            }
            else
            {
                Estoque = Estoque - quantidade;
            }

            var movimentacao = new Movimentacao();
            movimentacao.CodigoProduto = Codigo;
            movimentacao.Tipo = tipo;
            movimentacao.Descricao = descricao.Trim();
            movimentacao.Quantidade = quantidade;
            movimentacao.SaldoApos = Estoque;
            movimentacao.DataHora = DateTime.UtcNow;

            return movimentacao;
        }
    }
}
