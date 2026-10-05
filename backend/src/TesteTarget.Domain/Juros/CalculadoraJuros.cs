namespace TesteTarget.Domain.Juros
{
    public static class CalculadoraJuros
    {
        public const decimal TaxaDiariaPadrao = 0.025m;

        // Juros simples: valor x taxa diária x dias de atraso. Sem atraso, não há juros.
        public static ResultadoJuros Calcular(decimal valor, DateOnly vencimento, DateOnly hoje, decimal taxaDiaria)
        {
            int diasAtraso = hoje.DayNumber - vencimento.DayNumber;

            if (diasAtraso < 0)
            {
                diasAtraso = 0;
            }

            decimal juros = Math.Round(valor * taxaDiaria * diasAtraso, 2, MidpointRounding.AwayFromZero);

            var resultado = new ResultadoJuros();
            resultado.DiasAtraso = diasAtraso;
            resultado.Juros = juros;
            resultado.ValorTotal = valor + juros;

            return resultado;
        }
    }
}
