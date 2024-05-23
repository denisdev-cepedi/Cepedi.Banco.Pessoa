using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cepedi.Compartilhado.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("[controller]/v1/Telefones")]
// [Authorize]
public class TelefoneController : BaseController
{
    private readonly ILogger<TelefoneController> _logger;

    public TelefoneController(IMediator mediator, ILogger<TelefoneController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ObterTodosTelefonesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterTodosTelefonesResponse>> ObterTodosTelefonesAsync()
    {
        _logger.LogInformation("Obtendo todos os telefones");
        return await SendCommand(new ObterTodosTelefonesRequest());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterTelefoneResponse>> ObterTelefoneAsync([FromRoute] int id)
    {
        _logger.LogInformation($"Obtendo o telefone {id}");
        return await SendCommand(new ObterTelefoneRequest() { TelefoneId = id });
    }

    [HttpPost]
    [ProducesResponseType(typeof(CadastrarTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CadastrarTelefoneResponse>> CadastrarTelefoneAsync([FromBody] CadastrarTelefoneRequest request)
    {
        _logger.LogInformation("Cadastrando novo telefone");
        return await SendCommand(request);
    }

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarTelefoneResponse>> AtualizarTelefoneAsync([FromBody] AtualizarTelefoneRequest request)
    {
        _logger.LogInformation($"Atualizando o telefone {request.Id}");
        return await SendCommand(request);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ExcluirTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ExcluirTelefoneResponse>> ExcluirTelefoneAsync([FromBody] ExcluirTelefoneRequest request)
    {
        _logger.LogInformation($"Excluindo o telefone {request.TelefoneId}");
        return await SendCommand(request);
    }

}
