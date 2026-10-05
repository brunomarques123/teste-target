using Microsoft.EntityFrameworkCore;
using TesteTarget.Application.Comissoes;
using TesteTarget.Domain.Comissoes;
using TesteTarget.Infrastructure.Data;

namespace TesteTarget.Infrastructure.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly AppDbContext _db;

        public VendaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Venda>> ListarAsync()
        {
            return await _db.Vendas
                .AsNoTracking()
                .OrderBy(venda => venda.Id)
                .ToListAsync();
        }
    }
}
