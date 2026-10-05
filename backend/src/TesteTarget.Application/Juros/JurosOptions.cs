using TesteTarget.Domain.Juros;

namespace TesteTarget.Application.Juros
{
    // Seção "Juros" do appsettings.json.
    public class JurosOptions
    {
        public const string Secao = "Juros";

        public decimal TaxaDiaria { get; set; } = CalculadoraJuros.TaxaDiariaPadrao;
    }
}
