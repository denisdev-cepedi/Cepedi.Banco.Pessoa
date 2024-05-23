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

public class AtualizarTelefoneRequestHandlerTests
{
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarTelefoneRequestHandler> _logger = Substitute.For<ILogger<AtualizarTelefoneRequestHandler>>();
    private readonly AtualizarTelefoneRequestHandler _sut;

    public AtualizarTelefoneRequestHandlerTests()
    {
        _sut = new AtualizarTelefoneRequestHandler(_telefoneRepository, _pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task AtualizarTelefoneAsync_QuandoAtualizar_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new AtualizarTelefoneRequest
        {
            Id = 1,
            CodPais = "55",
            Ddd = "73",
            Numero = "123",
            Tipo = "Celular",
            Principal = true
        };

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

        _pessoaRepository.ObterTelefonePrincipalAsync(It.IsAny<int>()).ReturnsNullForAnyArgs();
        _telefoneRepository.ObterTelefoneAsync(It.IsAny<int>()).ReturnsForAnyArgs(new TelefoneEntity());
        _telefoneRepository.AtualizarTelefoneAsync(It.IsAny<TelefoneEntity>()).ReturnsForAnyArgs(telefoneEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<AtualizarTelefoneResponse>>().Which.Value.Numero.Should().Be(request.Numero);
        result.Should().BeOfType<Result<AtualizarTelefoneResponse>>().Which.Value.Numero.Should().NotBeEmpty();
    }

}
