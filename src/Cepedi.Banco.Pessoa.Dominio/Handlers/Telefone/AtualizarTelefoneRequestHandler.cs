using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class AtualizarTelefoneRequestHandler : IRequestHandler<AtualizarTelefoneRequest, Result<AtualizarTelefoneResponse>>
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly ILogger<AtualizarTelefoneRequestHandler> _logger;
    public AtualizarTelefoneRequestHandler(ITelefoneRepository telefoneRepository, ILogger<AtualizarTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarTelefoneResponse>> Handle(AtualizarTelefoneRequest request, CancellationToken cancellationToken)
    {
        var telefone = await _telefoneRepository.ObterTelefoneAsync(request.Id);
        if (telefone == null)
        {
            return Result.Error<AtualizarTelefoneResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
        }

        telefone.Atualizar(request);
        await _telefoneRepository.AtualizarTelefoneAsync(telefone);

        return Result.Success(new AtualizarTelefoneResponse()
        {
            Id = telefone.Id,
            CodPais = request.CodPais,
            Ddd = request.Ddd,
            Numero = request.Numero,
            Principal = request.Principal,
            Tipo = request.Tipo
        });
    }
}
