using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class ObterTodosTelefonesRequestHandler : IRequestHandler<ObterTodosTelefonesRequest, Result<ObterTodosTelefonesResponse>>
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly ILogger<ObterTodosTelefonesRequestHandler> _logger;
    public ObterTodosTelefonesRequestHandler(ITelefoneRepository telefoneRepository, ILogger<ObterTodosTelefonesRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _logger = logger;
    }

    public async Task<Result<ObterTodosTelefonesResponse>> Handle(ObterTodosTelefonesRequest request, CancellationToken cancellationToken)
    {
        var telefones = await _telefoneRepository.ObterTodosTelefonesAsync();

        _logger.LogInformation("Retornando lista de telefones");

        return Result.Success(new ObterTodosTelefonesResponse()
        {
            Telefones = telefones.Select(telefone => new ObterTelefoneResponse()
            {
                Id = telefone.Id,
                CodPais = telefone.CodPais,
                Ddd = telefone.Ddd,
                Numero = telefone.Numero,
                Principal = telefone.Principal,
                IdPessoa = telefone.IdPessoa,
                Tipo = telefone.Tipo
            }).ToList()
        });
    }
}
