using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cepedi.Banco.Pessoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private readonly IMediator _mediator;

        public PessoaController(ILogger<PessoaController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ObterTodasPessoasResponse>> ObterTodasPessoas()
        {
            return await _mediator.Send(new ObterTodasPessoasRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObterPessoaPorIdResponse>> ObterPessoaPorId(int id)
        {
            return await _mediator.Send(new ObterPessoaPorIdRequest { PessoaId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CadastrarPessoaResponse>> CadastrarPessoa([FromBody] CadastrarPessoaRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AtualizarPessoaResponse>> AtualizarPessoa(int id, [FromBody] AtualizarPessoaRequest request)
        {
            if (id != request.PessoaId)
            {
                return BadRequest();
            }

            return await _mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ExcluirPessoaResponse>> ExcluirPessoa(int id)
        {
            return await _mediator.Send(new ExcluirPessoaRequest { PessoaId = id });
        }
    }
}
