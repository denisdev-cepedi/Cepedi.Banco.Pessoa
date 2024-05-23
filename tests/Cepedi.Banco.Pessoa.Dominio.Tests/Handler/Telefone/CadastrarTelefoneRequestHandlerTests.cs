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

public class CadastrarTelefoneRequestHandlerTests
{
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CadastrarTelefoneRequestHandler> _logger = Substitute.For<ILogger<CadastrarTelefoneRequestHandler>>();
    private readonly CadastrarTelefoneRequestHandler _sut;

    public CadastrarTelefoneRequestHandlerTests()
    {
        _sut = new CadastrarTelefoneRequestHandler(_telefoneRepository, _pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task CadastrarTelefoneAsync_QuandoCadastrar_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new CadastrarTelefoneRequest
        {
            CodPais = "55",
            Ddd = "73",
            Numero = "123456789",
            Tipo = "Celular",
            IdPessoa = 1
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).ReturnsForAnyArgs(new PessoaEntity());
        _telefoneRepository.CadastrarTelefoneAsync(It.IsAny<TelefoneEntity>()).ReturnsForAnyArgs(new TelefoneEntity());
        _uinityOfWork.SaveChangesAsync().ReturnsForAnyArgs(true);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CadastrarTelefoneResponse>>().Which.Value.Numero.Should().Be(request.Numero);
        result.Should().BeOfType<Result<CadastrarTelefoneResponse>>().Which.Value.Numero.Should().NotBeEmpty();
    }

}
