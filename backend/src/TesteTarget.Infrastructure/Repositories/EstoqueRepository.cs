using Microsoft.EntityFrameworkCore;
using TesteTarget.Application.Estoque;
using TesteTarget.Domain;
using TesteTarget.Domain.Estoque;
using TesteTarget.Infrastructure.Data;

namespace TesteTarget.Infrastructure.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly AppDbContext _db;

        public EstoqueRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Produto>> ListarProdutosAsync()
        {
            return await _db.Produtos
                .AsNoTracking()
                .OrderBy(produto => produto.Codigo)
                .ToListAsync();
        }

        public async Task<Produto?> ObterProdutoAsync(int codigo)
        {
            return await _db.Produtos.FirstOrDefaultAsync(produto => produto.Codigo == codigo);
        }

        public async Task<List<Movimentacao>> ListarMovimentacoesAsync(int? codigoProduto)
        {
            var consulta = _db.Movimentacoes
                .AsNoTracking()
                .Include(movimentacao => movimentacao.Produto)
                .AsQueryable();

            if (codigoProduto.HasValue)
            {
                consulta = consulta.Where(movimentacao => movimentacao.CodigoProduto == codigoProduto.Value);
            }

            return await consulta
                .OrderByDescending(movimentacao => movimentacao.Id)
                .ToListAsync();
        }

        public async Task RegistrarMovimentacaoAsync(Produto produto, Movimentacao movimentacao)
        {
            // O produto já está rastreado com o saldo novo: um único SaveChanges grava os dois em transação.
            _db.Movimentacoes.Add(movimentacao);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new RegraDeNegocioException(
                    "O estoque deste produto foi alterado por outra operação. Tente novamente.");
            }
        }
    }
}
