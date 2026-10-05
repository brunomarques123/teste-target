using TesteTarget.Domain.Estoque;

namespace TesteTarget.Application.Estoque
{
    public interface IEstoqueRepository
    {
        Task<List<Produto>> ListarProdutosAsync();
        Task<Produto?> ObterProdutoAsync(int codigo);
        Task<List<Movimentacao>> ListarMovimentacoesAsync(int? codigoProduto);

        // Grava o saldo do produto e a movimentação na mesma transação.
        Task RegistrarMovimentacaoAsync(Produto produto, Movimentacao movimentacao);
    }
}
