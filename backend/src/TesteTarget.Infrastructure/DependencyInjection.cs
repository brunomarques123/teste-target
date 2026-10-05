using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TesteTarget.Application.Comissoes;
using TesteTarget.Application.Estoque;
using TesteTarget.Infrastructure.Data;
using TesteTarget.Infrastructure.Repositories;

namespace TesteTarget.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opcoes => opcoes.UseSqlServer(connectionString));

            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();

            return services;
        }
    }
}
