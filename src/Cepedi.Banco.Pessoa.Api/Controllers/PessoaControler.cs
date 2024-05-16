using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dados;
using MediatR;
using Cepedi.Compartilhado.Exceptions;
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
        [ProducesResponseType(typeof(ObterTodasPessoasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterTodasPessoasResponse>> ObterTodasPessoas()
        {
            return await SendCommand(new ObterTodasPessoasRequest());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterPessoaResponse>> ObterPessoa([FromRoute] int id)
        {
            return await SendCommand(new ObterPessoaRequest() { PessoaId = id });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CadastrarPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CadastrarPessoaResponse>> CadastrarPessoa([FromBody] CadastrarPessoaRequest request)
        {
            return await SendCommand(request);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AtualizarPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<AtualizarPessoaResponse>> AtualizarPessoa([FromBody] AtualizarPessoaRequest request)
        {
            return await SendCommand(request);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExcluirPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ExcluirPessoaResponse>> ExcluirPessoa([FromRoute] int id)
        {
            var request = new ExcluirPessoaRequest() { PessoaId = id };
            return await SendCommand(request);
        }

        [HttpGet("{id}/telefones")]
        [ProducesResponseType(typeof(ObterTelefonesPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterTelefonesPessoaResponse>> ObterTelefonesPessoa(int id)
        {
            var request = new ObterTelefonesPessoaRequest() { PessoaId = id };
            return await SendCommand(request);
        }

        [HttpGet("{id}/enderecos")]
        [ProducesResponseType(typeof(ObterEnderecosPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterEnderecosPessoaResponse>> ObterEnderecosPessoa(int id)
        {
            var request = new ObterEnderecosPessoaRequest() { PessoaId = id };
            return await SendCommand(request);
        }

    }
}
