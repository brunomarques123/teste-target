namespace TesteTarget.Application.Comissoes
{
    public interface IComissaoService
    {
        Task<ComissaoResponse> CalcularAsync();
    }
}
