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
        await _telefoneRepository.CadastrarTelefoneAsync(telefone);

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
        await _enderecoRepository.CadastrarEnderecoAsync(endereco);

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
            Nacionalidade = pessoa.Nacionalidade
        });
    }
}
