using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers
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
        public async Task<ActionResult<ObterPessoaResponse>> ObterPessoa([FromRoute] int id)
        {
            return await SendCommand(new ObterPessoaRequest() { PessoaId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CadastrarPessoaResponse>> CadastrarPessoa([FromBody] CadastrarPessoaRequest request)
        {
            return await SendCommand(request);
        }

    }
}