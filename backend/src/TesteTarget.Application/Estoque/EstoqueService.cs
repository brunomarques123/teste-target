using TesteTarget.Domain;
using TesteTarget.Domain.Estoque;

namespace TesteTarget.Application.Estoque
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;

        public EstoqueService(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProdutoResponse>> ListarProdutosAsync()
        {
            var produtos = await _repository.ListarProdutosAsync();

            var respostas = new List<ProdutoResponse>();

            foreach (var produto in produtos)
            {
                var resposta = new ProdutoResponse();
                resposta.Codigo = produto.Codigo;
                resposta.Descricao = produto.Descricao;
                resposta.Estoque = produto.Estoque;

                respostas.Add(resposta);
            }

            return respostas;
        }

        public async Task<MovimentacaoResponse> MovimentarAsync(MovimentacaoRequest request)
        {
            if (!Enum.IsDefined(request.Tipo))
            {
                throw new RegraDeNegocioException("Tipo de movimentação inválido. Use Entrada ou Saida.");
            }

            var produto = await _repository.ObterProdutoAsync(request.CodigoProduto);

            if (produto == null)
            {
                throw new RegraDeNegocioException($"Produto {request.CodigoProduto} não encontrado.");
            }

            var movimentacao = produto.Movimentar(request.Tipo, request.Quantidade, request.Descricao);

            await _repository.RegistrarMovimentacaoAsync(produto, movimentacao);

            return ParaResponse(movimentacao, produto.Descricao);
        }

        public async Task<List<MovimentacaoResponse>> ListarMovimentacoesAsync(int? codigoProduto)
        {
            var movimentacoes = await _repository.ListarMovimentacoesAsync(codigoProduto);

            var respostas = new List<MovimentacaoResponse>();

            foreach (var movimentacao in movimentacoes)
            {
                string descricaoProduto = string.Empty;

                if (movimentacao.Produto != null)
                {
                    descricaoProduto = movimentacao.Produto.Descricao;
                }

                respostas.Add(ParaResponse(movimentacao, descricaoProduto));
            }

            return respostas;
        }

        private static MovimentacaoResponse ParaResponse(Movimentacao movimentacao, string descricaoProduto)
        {
            var resposta = new MovimentacaoResponse();
            resposta.Id = movimentacao.Id;
            resposta.CodigoProduto = movimentacao.CodigoProduto;
            resposta.DescricaoProduto = descricaoProduto;
            resposta.Tipo = movimentacao.Tipo;
            resposta.Descricao = movimentacao.Descricao;
            resposta.Quantidade = movimentacao.Quantidade;
            resposta.DataHora = movimentacao.DataHora;
            resposta.SaldoFinal = movimentacao.SaldoApos;

            return resposta;
        }
    }
}
