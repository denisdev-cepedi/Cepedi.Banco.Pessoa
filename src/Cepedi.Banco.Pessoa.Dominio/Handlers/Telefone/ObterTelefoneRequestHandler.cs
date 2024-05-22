using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class ObterTelefoneRequestHandler : IRequestHandler<ObterTelefoneRequest, Result<ObterTelefoneResponse>>
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly ILogger<ObterTelefoneRequestHandler> _logger;
    public ObterTelefoneRequestHandler(ITelefoneRepository telefoneRepository, ILogger<ObterTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _logger = logger;
    }
    public async Task<Result<ObterTelefoneResponse>> Handle(ObterTelefoneRequest request, CancellationToken cancellationToken)
    {
        var telefone = await _telefoneRepository.ObterTelefoneAsync(request.TelefoneId);
        if (telefone == null)
        {
            return Result.Error<ObterTelefoneResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
        }

        return Result.Success(new ObterTelefoneResponse()
        {
            Id = telefone.Id,
            CodPais = telefone.CodPais,
            Ddd = telefone.Ddd,
            Numero = telefone.Numero,
            Tipo = telefone.Tipo,
            Principal = telefone.Principal
        });
    }
}
