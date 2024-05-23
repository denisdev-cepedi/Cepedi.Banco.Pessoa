using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class CadastrarEnderecoRequestHandler : IRequestHandler<CadastrarEnderecoRequest, Result<CadastrarEnderecoResponse>>
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CadastrarEnderecoRequestHandler> _logger;

    public CadastrarEnderecoRequestHandler(IEnderecoRepository enderecoRepository, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork, ILogger<CadastrarEnderecoRequestHandler> logger)
    {
        _enderecoRepository = enderecoRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<CadastrarEnderecoResponse>> Handle(CadastrarEnderecoRequest request, CancellationToken cancellationToken)
    {
        var _pessoa = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);

        if (_pessoa is null)
        {
            _logger.LogError("Pessoa não encontrada");
            return Result.Error<CadastrarEnderecoResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
        }

        var endereco = new EnderecoEntity()
        {
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Uf = request.Uf,
            Pais = request.Pais,
            Numero = request.Numero,
            Principal = false,
            IdPessoa = request.IdPessoa
        };

        await _enderecoRepository.CadastrarEnderecoAsync(endereco);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Endereço cadastrado");

        return Result.Success(new CadastrarEnderecoResponse()
        {
            Id = endereco.Id,
            Cep = endereco.Cep,
            Logradouro = endereco.Logradouro,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Uf = endereco.Uf,
            Pais = endereco.Pais,
            Numero = endereco.Numero,
            Principal = endereco.Principal,
            IdPessoa = endereco.IdPessoa
        });
    }
}
