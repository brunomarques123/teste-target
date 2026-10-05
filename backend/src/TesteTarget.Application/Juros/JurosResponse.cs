namespace TesteTarget.Application.Juros
{
    public class JurosResponse
    {
        public decimal Valor { get; set; }
        public DateOnly Vencimento { get; set; }
        public DateOnly DataCalculo { get; set; }
        public int DiasAtraso { get; set; }
        public decimal TaxaDiaria { get; set; }
        public decimal Juros { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
