using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dados;
using MediatR;
using OperationResult;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Pessoa.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TelefoneController : BaseController
{
    private readonly ILogger<TelefoneController> _logger;
    private readonly ApplicationDbContext _context;

    public TelefoneController(IMediator mediator, ILogger<TelefoneController> logger, ApplicationDbContext context) : base(mediator)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ObterTodosTelefonesResponse>> ObterTodosTelefones()
    {
        return await SendCommand(new ObterTodosTelefonesRequest());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObterTelefoneResponse>> ObterTelefone([FromRoute] int id)
    {
        return await SendCommand(new ObterTelefoneRequest() { TelefoneId = id });
    }

    [HttpGet("/id/{id}")]
    public async Task<ActionResult<ObterTelefonePorIdResponse>> ObterTelefonePorId([FromRoute] int id)
    {
        return await SendCommand(new ObterTelefonePorIdResponse() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<CadastrarTelefoneResponse>> CadastrarTelefone([FromBody] CadastrarTelefoneRequest request)
    {
        return await SendCommand(request);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<AtualizarTelefoneResponse>> AtualizarTelefone([FromBody] AtualizarTelefoneRequest request)
    {
        return await SendCommand(request);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ExcluirTelefoneResponse>> ExcluirTelefone([FromRoute] int id)
    {
        var request = new ExcluirTelefoneRequest() { TelefoneId = id };
        return await SendCommand(request);
    }

}