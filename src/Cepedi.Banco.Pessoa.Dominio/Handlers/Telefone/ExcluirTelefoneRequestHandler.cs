using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class ExcluirTelefoneRequestHandler : IRequestHandler<ExcluirTelefoneRequest, Result<ExcluirTelefoneResponse>>
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly ILogger<ExcluirTelefoneRequestHandler> _logger;
    public ExcluirTelefoneRequestHandler(ITelefoneRepository telefoneRepository, ILogger<ExcluirTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _logger = logger;
    }

    public async Task<Result<ExcluirTelefoneResponse>> Handle(ExcluirTelefoneRequest request, CancellationToken cancellationToken)
    {
        var telefone = await _telefoneRepository.ObterTelefoneAsync(request.TelefoneId);
        if (telefone == null)
        {
            return Result.Error<ExcluirTelefoneResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
        }

        await _telefoneRepository.ExcluirTelefoneAsync(telefone);

        return Result.Success(new ExcluirTelefoneResponse());
    }
}
