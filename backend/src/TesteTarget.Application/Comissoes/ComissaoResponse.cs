using TesteTarget.Domain.Comissoes;

namespace TesteTarget.Application.Comissoes
{
    public class ComissaoResponse
    {
        public List<ResumoVendedor> Vendedores { get; set; } = new List<ResumoVendedor>();
        public decimal TotalVendido { get; set; }
        public decimal TotalComissao { get; set; }
    }
}
