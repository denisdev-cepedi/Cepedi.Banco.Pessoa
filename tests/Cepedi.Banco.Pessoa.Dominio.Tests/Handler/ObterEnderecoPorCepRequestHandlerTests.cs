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

public class ObterEnderecoPorCepRequestHandlerTests
{
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly ILogger<ObterEnderecoPorCepRequestHandler> _logger = Substitute.For<ILogger<ObterEnderecoPorCepRequestHandler>>();
    private readonly ObterEnderecoPorCepRequestHandler _sut;

    public ObterEnderecoPorCepRequestHandlerTests()
    {
        _sut = new ObterEnderecoPorCepRequestHandler(_enderecoRepository, _logger);
    }

    [Fact]
    public async Task ObterEnderecoPorCepAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterEnderecoPorCepRequest { Cep = "45656000" };

        var enderecoEntity = new EnderecoEntity
        {
            Id = 1,
            Cep = "45656000",
            Logradouro = "Logradouro",
            Complemento = "Complemento",
            Bairro = "Bairro",
            Cidade = "Cidade",
            Uf = "UF",
            Pais = "Pais",
            Numero = "123",
            IdPessoa = 1
        };

        _enderecoRepository.ObterEnderecoPorCepAsync(It.IsAny<string>()).ReturnsForAnyArgs(enderecoEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterEnderecoPorCepResponse>>().Which.Value.Cep.Should().Be(request.Cep);
    }

}
