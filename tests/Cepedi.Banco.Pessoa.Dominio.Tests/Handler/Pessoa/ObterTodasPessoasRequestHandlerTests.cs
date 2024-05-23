using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Handlers;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Domain.Tests;

public class ObterTodasPessoasRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<ObterTodasPessoasRequestHandler> _logger = Substitute.For<ILogger<ObterTodasPessoasRequestHandler>>();
    private readonly ObterTodasPessoasRequestHandler _sut;

    public ObterTodasPessoasRequestHandlerTests()
    {
        _sut = new ObterTodasPessoasRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task ObterTodasPessoasAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterTodasPessoasRequest();

        var pessoas = new List<PessoaEntity>(){
            new PessoaEntity {
                Id = 1,
                Cpf = "12345678910",
                Email = "5wzr5@example.com",
                Nome = "Teste",
                DataNascimento = DateTime.Now,
                EstadoCivil = "Solteiro",
                Genero = "Masculino",
                Nacionalidade = "Brasileiro",
            },
            new PessoaEntity {
                Id = 2,
                Cpf = "12345678909",
                Email = "5wzr5@example.com",
                Nome = "Teste",
                DataNascimento = DateTime.Now,
                EstadoCivil = "Solteiro",
                Genero = "Masculino",
                Nacionalidade = "Brasileiro",
            }
        };

        _pessoaRepository.ObterTodasPessoasAsync().ReturnsForAnyArgs(pessoas);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterTodasPessoasResponse>>().Which.Value.Pessoas.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterTodasPessoasResponse>>().Which.Value.Pessoas.Count().Should().Be(2);
        result.Should().BeOfType<Result<ObterTodasPessoasResponse>>().Which.Value.Pessoas[0].Cpf.Should().Be("12345678910");
        result.Should().BeOfType<Result<ObterTodasPessoasResponse>>().Which.Value.Pessoas[1].Cpf.Should().Be("12345678909");
    }

}
