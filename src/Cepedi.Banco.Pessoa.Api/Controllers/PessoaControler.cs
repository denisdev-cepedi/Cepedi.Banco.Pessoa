using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers;
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : BaseController
    {
        private readonly ILogger<PessoaController> _logger;
        private readonly ApplicationDbContext _context;

        public PessoaController(IMediator mediator, ILogger<PessoaController> logger, ApplicationDbContext context) : base(mediator)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ObterTodasPessoasResponse>> ObterTodasPessoas()
        {
            return await SendCommand(new ObterTodasPessoasRequest());
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
