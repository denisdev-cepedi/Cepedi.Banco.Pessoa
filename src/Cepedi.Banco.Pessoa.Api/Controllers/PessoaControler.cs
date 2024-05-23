using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Cepedi.Compartilhado.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Cepedi.Banco.Pessoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]/v1/Pessoas")]
    // [Authorize]
    public class PessoaController : BaseController
    {
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(IMediator mediator, ILogger<PessoaController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ObterTodasPessoasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterTodasPessoasResponse>> ObterTodasPessoasAsync()
        {
            _logger.LogInformation("Obtendo todas as pessoas");
            return await SendCommand(new ObterTodasPessoasRequest());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterPessoaResponse>> ObterPessoaAsync([FromRoute] int id)
        {
            _logger.LogInformation($"Obtendo a pessoa {id}");
            return await SendCommand(new ObterPessoaRequest() { PessoaId = id });
        }

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(ObterPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterPessoaResponse>> ObterPessoaPorCpfAsync([FromRoute] string cpf)
        {
            _logger.LogInformation($"Obtendo a pessoa por cpf: {cpf}");
            return await SendCommand(new ObterPessoaPorCpfRequest() { Cpf = cpf });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CadastrarPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CadastrarPessoaResponse>> CadastrarPessoaAsync([FromBody] CadastrarPessoaRequest request)
        {
            _logger.LogInformation("Cadastrando nova pessoa");
            return await SendCommand(request);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AtualizarPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<AtualizarPessoaResponse>> AtualizarPessoaAsync([FromBody] AtualizarPessoaRequest request)
        {
            _logger.LogInformation($"Atualizando a pessoa {request.Id}");
            return await SendCommand(request);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ExcluirPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ExcluirPessoaResponse>> ExcluirPessoaAsync([FromBody] ExcluirPessoaRequest request)
        {
            _logger.LogInformation($"Excluindo a pessoa {request.PessoaId}");
            return await SendCommand(request);
        }

        [HttpGet("{id}/Telefones")]
        [ProducesResponseType(typeof(ObterTelefonesPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterTelefonesPessoaResponse>> ObterTelefonesPessoa(int id)
        {
            _logger.LogInformation($"Obtendo os telefones da pessoa {id}");
            var request = new ObterTelefonesPessoaRequest() { PessoaId = id };
            return await SendCommand(request);
        }

        [HttpGet("{id}/Enderecos")]
        [ProducesResponseType(typeof(ObterEnderecosPessoaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ObterEnderecosPessoaResponse>> ObterEnderecosPessoa(int id)
        {
            _logger.LogInformation($"Obtendo os endereços da pessoa {id}");
            var request = new ObterEnderecosPessoaRequest() { PessoaId = id };
            return await SendCommand(request);
        }
    }
}
