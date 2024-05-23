using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class CadastrarTelefoneRequestHandler : IRequestHandler<CadastrarTelefoneRequest, Result<CadastrarTelefoneResponse>>
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CadastrarTelefoneRequestHandler> _logger;
    public CadastrarTelefoneRequestHandler(ITelefoneRepository telefoneRepository, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork, ILogger<CadastrarTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<CadastrarTelefoneResponse>> Handle(CadastrarTelefoneRequest request, CancellationToken cancellationToken)
    {
        var _pessoa = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);

        if (_pessoa is null)
        {
            _logger.LogError("Pessoa não encontrada");
            return Result.Error<CadastrarTelefoneResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
        }

        var telefone = new TelefoneEntity()
        {
            CodPais = request.CodPais,
            Ddd = request.Ddd,
            Numero = request.Numero,
            Tipo = request.Tipo,
            Principal = false,
            IdPessoa = request.IdPessoa
        };

        await _telefoneRepository.CadastrarTelefoneAsync(telefone);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Telefone cadastrado");

        return Result.Success(new CadastrarTelefoneResponse()
        {
            Id = telefone.Id,
            Ddd = telefone.Ddd,
            Numero = telefone.Numero,
            Tipo = telefone.Tipo,
            Principal = telefone.Principal,
            IdPessoa = telefone.IdPessoa
        });
    }
}
