using Microsoft.AspNetCore.Mvc;
using TesteTarget.Application.Juros;

namespace TesteTarget.Api.Controllers
{
    [ApiController]
    [Route("api/juros")]
    public class JurosController : ControllerBase
    {
        private readonly IJurosService _service;

        public JurosController(IJurosService service)
        {
            _service = service;
        }

        [HttpPost("calcular")]
        public ActionResult<JurosResponse> Calcular(JurosRequest request)
        {
            var resultado = _service.Calcular(request);

            return Ok(resultado);
        }
    }
}
