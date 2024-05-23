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

public class ObterTodosEnderecosRequestHandlerTests
{
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly ILogger<ObterTodosEnderecosRequestHandler> _logger = Substitute.For<ILogger<ObterTodosEnderecosRequestHandler>>();
    private readonly ObterTodosEnderecosRequestHandler _sut;

    public ObterTodosEnderecosRequestHandlerTests()
    {
        _sut = new ObterTodosEnderecosRequestHandler(_enderecoRepository, _logger);
    }

    [Fact]
    public async Task ObterTodosEnderecosAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterTodosEnderecosRequest();

        var enderecos = new List<EnderecoEntity>(){
            new EnderecoEntity {
                Id = 1,
                Cep = "45656000",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123",
                Principal = true,
                IdPessoa = 1
            },
            new EnderecoEntity {
                Id = 2,
                Cep = "45655000",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123",
                Principal = false,
                IdPessoa = 1
            }
        };

        _enderecoRepository.ObterTodosEnderecosAsync().ReturnsForAnyArgs(enderecos);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterTodosEnderecosResponse>>().Which.Value.Enderecos.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterTodosEnderecosResponse>>().Which.Value.Enderecos.Count().Should().Be(2);
        result.Should().BeOfType<Result<ObterTodosEnderecosResponse>>().Which.Value.Enderecos[0].Cep.Should().Be("45656000");
        result.Should().BeOfType<Result<ObterTodosEnderecosResponse>>().Which.Value.Enderecos[1].Cep.Should().Be("45655000");
    }

}
