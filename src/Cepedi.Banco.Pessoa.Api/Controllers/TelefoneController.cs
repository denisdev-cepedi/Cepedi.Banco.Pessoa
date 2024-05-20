using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cepedi.Compartilhado.Exceptions;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("banco-pessoa/v1/telefones")]
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
    public async Task<ActionResult<ObterTodosTelefonesResponse>> ObterTodosTelefones()
    {
        return await SendCommand(new ObterTodosTelefonesRequest());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterTelefoneResponse>> ObterTelefone([FromRoute] int id)
    {
        return await SendCommand(new ObterTelefoneRequest() { TelefoneId = id });
    }

    [HttpPost]
    [ProducesResponseType(typeof(CadastrarTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CadastrarTelefoneResponse>> CadastrarTelefone([FromBody] CadastrarTelefoneRequest request)
    {
        return await SendCommand(request);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AtualizarTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarTelefoneResponse>> AtualizarTelefone([FromBody] AtualizarTelefoneRequest request)
    {
        return await SendCommand(request);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ExcluirTelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ExcluirTelefoneResponse>> ExcluirTelefone([FromRoute] int id)
    {
        var request = new ExcluirTelefoneRequest() { TelefoneId = id };
        return await SendCommand(request);
    }

}
