using TesteTarget.Domain.Comissoes;

namespace TesteTarget.Application.Comissoes
{
    public interface IVendaRepository
    {
        Task<List<Venda>> ListarAsync();
    }
}
