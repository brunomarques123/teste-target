using TesteTarget.Domain.Estoque;

namespace TesteTarget.Application.Estoque
{
    public class MovimentacaoRequest
    {
        public int CodigoProduto { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
