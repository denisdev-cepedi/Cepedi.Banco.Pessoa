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

public class ObterEnderecoRequestHandlerTests
{
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly ILogger<ObterEnderecoRequestHandler> _logger = Substitute.For<ILogger<ObterEnderecoRequestHandler>>();
    private readonly ObterEnderecoRequestHandler _sut;

    public ObterEnderecoRequestHandlerTests()
    {
        _sut = new ObterEnderecoRequestHandler(_enderecoRepository, _logger);
    }

    [Fact]
    public async Task ObterEnderecoAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterEnderecoRequest { EnderecoId = 1 };

        var enderecoEntity = new EnderecoEntity
        {
            Id = 1,
            Cep = "12345678",
            Logradouro = "Logradouro",
            Complemento = "Complemento",
            Bairro = "Bairro",
            Cidade = "Cidade",
            Uf = "UF",
            Pais = "Pais",
            Numero = "123",
            Principal = true,
            IdPessoa = 1
        };

        _enderecoRepository.ObterEnderecoAsync(It.IsAny<int>()).ReturnsForAnyArgs(enderecoEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterEnderecoResponse>>().Which.Value.Id.Should().Be(request.EnderecoId);
    }

}
