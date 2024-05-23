using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Compartilhado.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("[controller]/v1/Enderecos")]
public class EnderecoController : BaseController
{
    private readonly ILogger<EnderecoController> _logger;

    public EnderecoController(IMediator mediator, ILogger<EnderecoController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ObterTodosEnderecosResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterTodosEnderecosResponse>> ObterTodosEnderecosAsync()
    {
        return await SendCommand(new ObterTodosEnderecosRequest());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterEnderecoResponse>> ObterEnderecoAsync([FromRoute] int id)
    {
        
        return await SendCommand(new ObterEnderecoRequest() { EnderecoId = id });
    }

    [HttpGet("Cep/{cep}")]
    [ProducesResponseType(typeof(ObterEnderecoPorCepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterEnderecoPorCepResponse>> ObterEnderecoPorCepAsync([FromRoute] string cep)
    {
        return await SendCommand(new ObterEnderecoPorCepRequest() { Cep = cep });
    }

    [HttpPost]
    [ProducesResponseType(typeof(CadastrarEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CadastrarEnderecoResponse>> CadastrarEnderecoAsync([FromBody] CadastrarEnderecoRequest request)
    {
        return await SendCommand(request);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AtualizarEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarEnderecoResponse>> AtualizarEnderecoAsync([FromBody] AtualizarEnderecoRequest request)
    {
        return await SendCommand(request);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ExcluirEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ExcluirEnderecoResponse>> ExcluirEnderecoAsync([FromRoute] int id)
    {
        var request = new ExcluirEnderecoRequest() { EnderecoId = id };
        return await SendCommand(request);
    }

}
