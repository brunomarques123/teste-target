namespace TesteTarget.Domain
{
    // Violação de regra de negócio. A API a converte em HTTP 422.
    public class RegraDeNegocioException : Exception
    {
        public RegraDeNegocioException(string mensagem) : base(mensagem)
        {
        }
    }
}
