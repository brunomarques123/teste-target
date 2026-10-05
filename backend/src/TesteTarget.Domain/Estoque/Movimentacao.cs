namespace TesteTarget.Domain.Estoque
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public int CodigoProduto { get; set; }
        public Produto? Produto { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public int SaldoApos { get; set; }
        public DateTime DataHora { get; set; }
    }
}
