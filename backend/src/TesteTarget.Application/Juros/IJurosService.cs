namespace TesteTarget.Application.Juros
{
    public interface IJurosService
    {
        JurosResponse Calcular(JurosRequest request);
    }
}
