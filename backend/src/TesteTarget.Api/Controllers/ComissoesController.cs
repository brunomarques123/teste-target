using Microsoft.AspNetCore.Mvc;
using TesteTarget.Application.Comissoes;

namespace TesteTarget.Api.Controllers
{
    [ApiController]
    [Route("api/comissoes")]
    public class ComissoesController : ControllerBase
    {
        private readonly IComissaoService _service;

        public ComissoesController(IComissaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ComissaoResponse>> Calcular()
        {
            var resultado = await _service.CalcularAsync();

            return Ok(resultado);
        }
    }
}
