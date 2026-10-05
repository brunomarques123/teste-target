namespace TesteTarget.Domain.Comissoes
{
    public class ResumoVendedor
    {
        public string Vendedor { get; set; } = string.Empty;
        public int QuantidadeVendas { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal TotalComissao { get; set; }
    }
}
