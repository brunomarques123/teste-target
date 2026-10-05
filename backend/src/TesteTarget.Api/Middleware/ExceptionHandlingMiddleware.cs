using Microsoft.AspNetCore.Mvc;
using TesteTarget.Domain;

namespace TesteTarget.Api.Middleware
{
    // Converte exceções em respostas ProblemDetails: 422 para regra de negócio, 500 para o restante.
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _proximo;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate proximo, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _proximo = proximo;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _proximo(context);
            }
            catch (RegraDeNegocioException ex)
            {
                await ResponderAsync(context, StatusCodes.Status422UnprocessableEntity, "Regra de negócio violada", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado");

                await ResponderAsync(context, StatusCodes.Status500InternalServerError, "Erro interno",
                    "Ocorreu um erro inesperado. Tente novamente.");
            }
        }

        private static async Task ResponderAsync(HttpContext context, int status, string titulo, string detalhe)
        {
            var problema = new ProblemDetails();
            problema.Status = status;
            problema.Title = titulo;
            problema.Detail = detalhe;

            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(problema);
        }
    }
}
