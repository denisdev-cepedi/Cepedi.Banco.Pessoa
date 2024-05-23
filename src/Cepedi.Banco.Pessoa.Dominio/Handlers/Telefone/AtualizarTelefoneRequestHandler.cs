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
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AtualizarTelefoneRequestHandler> _logger;
    public AtualizarTelefoneRequestHandler(ITelefoneRepository telefoneRepository, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork, ILogger<AtualizarTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<AtualizarTelefoneResponse>> Handle(AtualizarTelefoneRequest request, CancellationToken cancellationToken)
    {
        var telefone = await _telefoneRepository.ObterTelefoneAsync(request.Id);

        if (telefone is null)
        {
            return Result.Error<AtualizarTelefoneResponse>(new Compartilhado.Exceptions.TelefoneNaoEncontradoExcecao());
        }

        var telefonePrincipal = await _pessoaRepository.ObterTelefonePrincipalAsync(telefone.IdPessoa);

        if (request.Principal == false && (telefonePrincipal is null || telefone.Id == telefonePrincipal.Id))
        {
            return Result.Error<AtualizarTelefoneResponse>(new Compartilhado.Exceptions.MinimoUmTelefonePrincipalException());
        }

        if (telefonePrincipal is not null && request.Principal == true)
        {
            telefonePrincipal.Principal = false;
            await _telefoneRepository.AtualizarTelefoneAsync(telefonePrincipal);
        }

        telefone.Atualizar(request);
        await _telefoneRepository.AtualizarTelefoneAsync(telefone);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(new AtualizarTelefoneResponse()
        {
            Id = telefone.Id,
            CodPais = request.CodPais,
            Ddd = request.Ddd,
            Numero = request.Numero,
            Tipo = request.Tipo,
            Principal = request.Principal
        });
    }
}
