using Microsoft.AspNetCore.Mvc;
using TesteTarget.Application.Estoque;

namespace TesteTarget.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _service;

        public EstoqueController(IEstoqueService service)
        {
            _service = service;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<List<ProdutoResponse>>> Produtos()
        {
            var produtos = await _service.ListarProdutosAsync();

            return Ok(produtos);
        }

        [HttpGet("estoque/movimentacoes")]
        public async Task<ActionResult<List<MovimentacaoResponse>>> Movimentacoes([FromQuery] int? produto)
        {
            var movimentacoes = await _service.ListarMovimentacoesAsync(produto);

            return Ok(movimentacoes);
        }

        [HttpPost("estoque/movimentacoes")]
        public async Task<ActionResult<MovimentacaoResponse>> Movimentar(MovimentacaoRequest request)
        {
            var movimentacao = await _service.MovimentarAsync(request);

            return Ok(movimentacao);
        }
    }
}
