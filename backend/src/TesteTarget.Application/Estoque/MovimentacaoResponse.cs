using TesteTarget.Domain.Estoque;

namespace TesteTarget.Application.Estoque
{
    public class MovimentacaoResponse
    {
        public int Id { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; } = string.Empty;
        public TipoMovimentacao Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataHora { get; set; }
        public int SaldoFinal { get; set; }
    }
}
