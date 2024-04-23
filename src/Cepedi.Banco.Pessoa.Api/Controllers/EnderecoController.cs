using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : BaseController
{
    private readonly ILogger<EnderecoController> _logger;

    public EnderecoController(IMediator mediator, ILogger<EnderecoController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ObterTodosEnderecosResponse>> ObterTodosEnderecosAsync()
    {
        return await SendCommand(new ObterTodosEnderecosRequest());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObterEnderecoResponse>> ObterEnderecoAsync([FromRoute] int id)
    {
        return await SendCommand(new ObterEnderecoRequest() { EnderecoId = id });
    }

    [HttpGet("cep/{cep}")]
    public async Task<ActionResult<ObterEnderecoPorCepResponse>> ObterEnderecoPorCepAsync([FromRoute] string cep)
    {
        return await SendCommand(new ObterEnderecoPorCepRequest() { Cep = cep });
    }

    [HttpPost]
    public async Task<ActionResult<CadastrarEnderecoResponse>> CadastrarEnderecoAsync([FromBody] CadastrarEnderecoRequest request)
    {
        return await SendCommand(request);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AtualizarEnderecoResponse>> AtualizarEnderecoAsync([FromBody] AtualizarEnderecoRequest request)
    {
        return await SendCommand(request);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ExcluirEnderecoResponse>> ExcluirEnderecoAsync([FromRoute] int id)
    {
        var request = new ExcluirEnderecoRequest() { EnderecoId = id };
        return await SendCommand(request);
    }

}
