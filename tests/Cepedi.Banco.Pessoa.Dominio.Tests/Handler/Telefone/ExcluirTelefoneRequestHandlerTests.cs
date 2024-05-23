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

public class ExcluirTelefonePorCepRequestHandlerTests
{
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<ExcluirTelefoneRequestHandler> _logger = Substitute.For<ILogger<ExcluirTelefoneRequestHandler>>();
    private readonly ExcluirTelefoneRequestHandler _sut;

    public ExcluirTelefonePorCepRequestHandlerTests()
    {
        _sut = new ExcluirTelefoneRequestHandler(_telefoneRepository, _pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task ExcluirTelefoneAsync_QuandoExcluir_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ExcluirTelefoneRequest { TelefoneId = 1 };

        var telefoneEntity = new TelefoneEntity
        {
            Id = 1,
            CodPais = "55",
            Ddd = "73",
            Numero = "123",
            Tipo = "Celular",
            IdPessoa = 1
        };

        _telefoneRepository.ObterTelefoneAsync(request.TelefoneId).ReturnsForAnyArgs(telefoneEntity);
        _pessoaRepository.ObterTelefonePrincipalAsync(It.IsAny<int>()).ReturnsForAnyArgs(new TelefoneEntity() { Id = 2 });
        _telefoneRepository.ExcluirTelefoneAsync(It.IsAny<TelefoneEntity>()).ReturnsForAnyArgs(telefoneEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ExcluirTelefoneResponse>>().Which.Value.Should().NotBeNull();
    }

}
