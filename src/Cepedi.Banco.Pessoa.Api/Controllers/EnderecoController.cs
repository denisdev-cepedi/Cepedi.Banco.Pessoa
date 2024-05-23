using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Compartilhado.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("[controller]/v1/Enderecos")]
// [Authorize]
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
        _logger.LogInformation("Obtendo todos os endereços");
        return await SendCommand(new ObterTodosEnderecosRequest());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterEnderecoResponse>> ObterEnderecoAsync([FromRoute] int id)
    {
        _logger.LogInformation($"Obtendo o endereço {id}");
        return await SendCommand(new ObterEnderecoRequest() { EnderecoId = id });
    }

    [HttpGet("Cep/{cep}")]
    [ProducesResponseType(typeof(ObterEnderecoPorCepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ObterEnderecoPorCepResponse>> ObterEnderecoPorCepAsync([FromRoute] string cep)
    {
        _logger.LogInformation($"Obtendo o endereço por CEP: {cep}");
        return await SendCommand(new ObterEnderecoPorCepRequest() { Cep = cep });
    }

    [HttpPost]
    [ProducesResponseType(typeof(CadastrarEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CadastrarEnderecoResponse>> CadastrarEnderecoAsync([FromBody] CadastrarEnderecoRequest request)
    {
        _logger.LogInformation("Cadastrando novo endereço");
        return await SendCommand(request);
    }

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarEnderecoResponse>> AtualizarEnderecoAsync([FromBody] AtualizarEnderecoRequest request)
    {
        _logger.LogInformation($"Atualizando o endereço {request.Id}");
        return await SendCommand(request);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ExcluirEnderecoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ExcluirEnderecoResponse>> ExcluirEnderecoAsync([FromBody] ExcluirEnderecoRequest request)
    {
        _logger.LogInformation($"Excluindo o endereço {request.EnderecoId}");

        return await SendCommand(request);
    }

}
