using TesteTarget.Domain.Comissoes;

namespace TesteTarget.Application.Comissoes
{
    public class ComissaoService : IComissaoService
    {
        private readonly IVendaRepository _vendaRepository;

        public ComissaoService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public async Task<ComissaoResponse> CalcularAsync()
        {
            var vendas = await _vendaRepository.ListarAsync();
            var vendedores = CalculadoraComissao.CalcularPorVendedor(vendas);

            var resposta = new ComissaoResponse();
            resposta.Vendedores = vendedores;

            foreach (var vendedor in vendedores)
            {
                resposta.TotalVendido += vendedor.TotalVendido;
                resposta.TotalComissao += vendedor.TotalComissao;
            }

            return resposta;
        }
    }
}
