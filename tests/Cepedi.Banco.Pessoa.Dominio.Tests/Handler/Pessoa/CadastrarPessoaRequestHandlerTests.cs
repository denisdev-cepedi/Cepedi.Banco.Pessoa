using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Handlers;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Domain.Tests;

public class CadastrarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CadastrarPessoaRequestHandler> _logger = Substitute.For<ILogger<CadastrarPessoaRequestHandler>>();
    private readonly CadastrarPessoaRequestHandler _sut;

    public CadastrarPessoaRequestHandlerTests()
    {
        _sut = new CadastrarPessoaRequestHandler(_pessoaRepository, _telefoneRepository, _enderecoRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task CadastrarPessoaAsync_QuandoCadastrar_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new CadastrarPessoaRequest
        {
            Cpf = "12345678909",
            Email = "5wzr5@example.com",
            Nome = "Teste",
            DataNascimento = DateTime.Now,
            EstadoCivil = "Solteiro",
            Genero = "Masculino",
            Nacionalidade = "Brasileiro",
            Telefone = new CadastrarTelefonePessoaRequest
            {
                CodPais = "55",
                Ddd = "11",
                Numero = "999999999",
                Tipo = "Celular"
            },
            Endereco = new CadastrarEnderecoPessoaRequest
            {
                Bairro = "Bairro",
                Cep = "00000000",
                Cidade = "Cidade",
                Complemento = "Complemento",
                Uf = "SP",
                Logradouro = "Logradouro",
                Numero = "123",
                Pais = "Brasil"
            }
        };
        _enderecoRepository.CadastrarEnderecoAsync(It.IsAny<EnderecoEntity>()).ReturnsForAnyArgs(new EnderecoEntity());
        _telefoneRepository.CadastrarTelefoneAsync(It.IsAny<TelefoneEntity>()).ReturnsForAnyArgs(new TelefoneEntity());
        _pessoaRepository.ObterPessoaPorCpfAsync(request.Cpf).ReturnsNull();
        _pessoaRepository.CadastrarPessoaAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(new PessoaEntity());
        _uinityOfWork.SaveChangesAsync().ReturnsForAnyArgs(true);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CadastrarPessoaResponse>>().Which.Value.Cpf.Should().Be(request.Cpf);
        result.Should().BeOfType<Result<CadastrarPessoaResponse>>().Which.Value.Cpf.Should().NotBeEmpty();
    }

}
