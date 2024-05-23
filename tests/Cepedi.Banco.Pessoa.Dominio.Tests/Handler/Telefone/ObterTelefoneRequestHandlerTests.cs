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

public class ObterTelefoneRequestHandlerTests
{
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly ILogger<ObterTelefoneRequestHandler> _logger = Substitute.For<ILogger<ObterTelefoneRequestHandler>>();
    private readonly ObterTelefoneRequestHandler _sut;

    public ObterTelefoneRequestHandlerTests()
    {
        _sut = new ObterTelefoneRequestHandler(_telefoneRepository, _logger);
    }

    [Fact]
    public async Task ObterTelefoneAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterTelefoneRequest { TelefoneId = 1 };

        var telefoneEntity = new TelefoneEntity
        {
            Id = 1,
            CodPais = "55",
            Ddd = "73",
            Numero = "123456789",
            Tipo = "Celular",
            Principal = true,
            IdPessoa = 1
        };

        _telefoneRepository.ObterTelefoneAsync(It.IsAny<int>()).ReturnsForAnyArgs(telefoneEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterTelefoneResponse>>().Which.Value.Id.Should().Be(request.TelefoneId);
    }

}
