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
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ExcluirTelefoneRequestHandler> _logger;
    public ExcluirTelefoneRequestHandler(ITelefoneRepository telefoneRepository, IPessoaRepository pessoaRepository, ILogger<ExcluirTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<ExcluirTelefoneResponse>> Handle(ExcluirTelefoneRequest request, CancellationToken cancellationToken)
    {
        var telefone = await _telefoneRepository.ObterTelefoneAsync(request.TelefoneId);

        if (telefone is null)
        {
            return Result.Error<ExcluirTelefoneResponse>(new Compartilhado.Exceptions.TelefoneNaoEncontradoExcecao());
        }

        var telefonePrincipal = await _pessoaRepository.ObterTelefonePrincipalAsync(telefone.IdPessoa);

        if (telefonePrincipal is not null && request.TelefoneId == telefonePrincipal.Id)
        {
            return Result.Error<ExcluirTelefoneResponse>(new Compartilhado.Exceptions.ExclusaoTelefonePrincipalException());
        }

        await _telefoneRepository.ExcluirTelefoneAsync(telefone);

        return Result.Success(new ExcluirTelefoneResponse());
    }
}
