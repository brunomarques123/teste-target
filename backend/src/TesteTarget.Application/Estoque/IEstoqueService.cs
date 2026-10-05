namespace TesteTarget.Application.Estoque
{
    public interface IEstoqueService
    {
        Task<List<ProdutoResponse>> ListarProdutosAsync();

        Task<MovimentacaoResponse> MovimentarAsync(MovimentacaoRequest request);

        Task<List<MovimentacaoResponse>> ListarMovimentacoesAsync(int? codigoProduto);
    }
}
