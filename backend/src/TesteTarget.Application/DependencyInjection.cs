using Microsoft.Extensions.DependencyInjection;
using TesteTarget.Application.Comissoes;
using TesteTarget.Application.Estoque;
using TesteTarget.Application.Juros;

namespace TesteTarget.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(TimeProvider.System);

            services.AddScoped<IComissaoService, ComissaoService>();
            services.AddScoped<IJurosService, JurosService>();
            services.AddScoped<IEstoqueService, EstoqueService>();

            return services;
        }
    }
}
