using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

public class CadastrarPessoaRequestHandler : IRequestHandler<CadastrarPessoaRequest, Result<CadastrarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CadastrarPessoaRequestHandler> _logger;

    public CadastrarPessoaRequestHandler(
        IPessoaRepository pessoaRepository,
        ITelefoneRepository telefoneRepository,
        IEnderecoRepository enderecoRepository,
        IUnitOfWork unitOfWork,
        ILogger<CadastrarPessoaRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _telefoneRepository = telefoneRepository;
        _enderecoRepository = enderecoRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<CadastrarPessoaResponse>> Handle(CadastrarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoaExistente = await _pessoaRepository.ObterPessoaPorCpfAsync(request.Cpf);

        if (pessoaExistente is not null)
        {
            _logger.LogError("Cpf já existe");
           return Result.Error<CadastrarPessoaResponse>(new Cepedi.Banco.Pessoa.Compartilhado.Exceptions.CpfJaExisteExcecao());
        }

        var pessoa = new PessoaEntity
        {
            Nome = request.Nome,
            Email = request.Email,
            DataNascimento = request.DataNascimento,
            Cpf = request.Cpf,
            Genero = request.Genero,
            EstadoCivil = request.EstadoCivil,
            Nacionalidade = request.Nacionalidade
        };

        await _pessoaRepository.CadastrarPessoaAsync(pessoa);

        var telefone = new TelefoneEntity
        {
            CodPais = request.Telefone.CodPais,
            Ddd = request.Telefone.Ddd,
            Numero = request.Telefone.Numero,
            Tipo = request.Telefone.Tipo,
            Principal = true,
            Pessoa = pessoa
        };
        var _telefone = await _telefoneRepository.CadastrarTelefoneAsync(telefone);

        var endereco = new EnderecoEntity
        {
            Cep = request.Endereco.Cep,
            Logradouro = request.Endereco.Logradouro,
            Complemento = request.Endereco.Complemento,
            Bairro = request.Endereco.Bairro,
            Cidade = request.Endereco.Cidade,
            Uf = request.Endereco.Uf,
            Pais = request.Endereco.Pais,
            Numero = request.Endereco.Numero,
            Principal = true,
            Pessoa = pessoa
        };
        var _endereco = await _enderecoRepository.CadastrarEnderecoAsync(endereco);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Pessoa cadastrada com telefone e endereço");

        return Result.Success(new CadastrarPessoaResponse
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Email = pessoa.Email,
            DataNascimento = pessoa.DataNascimento,
            Cpf = pessoa.Cpf,
            Genero = pessoa.Genero,
            EstadoCivil = pessoa.EstadoCivil,
            Nacionalidade = pessoa.Nacionalidade,
            TelefonePrincipal = new CadastrarTelefoneResponse
            {
                Id = _telefone.Id,
                CodPais = _telefone.CodPais,
                Ddd = _telefone.Ddd,
                Numero = _telefone.Numero,
                Tipo = _telefone.Tipo,
                Principal = _telefone.Principal,
                IdPessoa = _telefone.IdPessoa
            },
            EnderecoPrincipal = new CadastrarEnderecoResponse
            {
                Id = _endereco.Id,
                Cep = _endereco.Cep,
                Logradouro = _endereco.Logradouro,
                Complemento = _endereco.Complemento,
                Bairro = _endereco.Bairro,
                Cidade = _endereco.Cidade,
                Uf = _endereco.Uf,
                Pais = _endereco.Pais,
                Numero = _endereco.Numero,
                Principal = _endereco.Principal,
                IdPessoa = _endereco.IdPessoa
            }
        });
    }
}
