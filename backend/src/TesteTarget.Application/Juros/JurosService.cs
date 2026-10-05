using Microsoft.Extensions.Options;
using TesteTarget.Domain;
using TesteTarget.Domain.Juros;

namespace TesteTarget.Application.Juros
{
    public class JurosService : IJurosService
    {
        private readonly TimeProvider _relogio;
        private readonly JurosOptions _opcoes;

        public JurosService(TimeProvider relogio, IOptions<JurosOptions> opcoes)
        {
            _relogio = relogio;
            _opcoes = opcoes.Value;
        }

        public JurosResponse Calcular(JurosRequest request)
        {
            if (request.Valor <= 0)
            {
                throw new RegraDeNegocioException("O valor deve ser maior que zero.");
            }

            var hoje = DateOnly.FromDateTime(_relogio.GetLocalNow().DateTime);
            var taxa = _opcoes.TaxaDiaria;
            var resultado = CalculadoraJuros.Calcular(request.Valor, request.Vencimento, hoje, taxa);

            var resposta = new JurosResponse();
            resposta.Valor = request.Valor;
            resposta.Vencimento = request.Vencimento;
            resposta.DataCalculo = hoje;
            resposta.DiasAtraso = resultado.DiasAtraso;
            resposta.TaxaDiaria = taxa;
            resposta.Juros = resultado.Juros;
            resposta.ValorTotal = resultado.ValorTotal;

            return resposta;
        }
    }
}
