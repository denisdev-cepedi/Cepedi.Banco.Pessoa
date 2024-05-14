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
    private readonly ILogger<CadastrarTelefoneRequestHandler> _logger;
    public CadastrarTelefoneRequestHandler(ITelefoneRepository telefoneRepository, ILogger<CadastrarTelefoneRequestHandler> logger)
    {
        _telefoneRepository = telefoneRepository;
        _logger = logger;
    }
    public async Task<Result<CadastrarTelefoneResponse>> Handle(CadastrarTelefoneRequest request, CancellationToken cancellationToken)
    {
        // ToDo: Verificar se a pessoa existe
        // var _pessoa = await pessoaRepository.ObterPessoaAsync(request.IdPessoa);

        // if (_pessoa is null)
        // {
        //     return Result.Error<CadastrarTelefoneResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
        // }

        var telefone = new TelefoneEntity()
        {
            CodPais = request.CodPais,
            Ddd = request.Ddd,
            Numero = request.Numero,
            Principal = request.Principal,
            Tipo = request.Tipo,
            IdPessoa = request.IdPessoa
        };

        await _telefoneRepository.CadastrarTelefoneAsync(telefone);

        return Result.Success(new CadastrarTelefoneResponse()
        {
            Id = telefone.Id,
            Ddd = request.Ddd,
            Numero = request.Numero,
            Principal = request.Principal,
            Tipo = request.Tipo,
            IdPessoa = request.IdPessoa
        });
    }
}
