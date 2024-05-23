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

public class ObterTodosTelefonesRequestHandlerTests
{
    private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
    private readonly ILogger<ObterTodosTelefonesRequestHandler> _logger = Substitute.For<ILogger<ObterTodosTelefonesRequestHandler>>();
    private readonly ObterTodosTelefonesRequestHandler _sut;

    public ObterTodosTelefonesRequestHandlerTests()
    {
        _sut = new ObterTodosTelefonesRequestHandler(_telefoneRepository, _logger);
    }

    [Fact]
    public async Task ObterTodosTelefonesAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterTodosTelefonesRequest();

        var telefones = new List<TelefoneEntity>(){
            new TelefoneEntity {
                Id = 1,
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
                Principal = true,
                IdPessoa = 1,
            },
            new TelefoneEntity {
                Id = 2,
                CodPais = "55",
                Ddd = "73",
                Tipo = "Celular",
                Numero = "987654321",
                Principal = false,
                IdPessoa = 1
            }
        };

        _telefoneRepository.ObterTodosTelefonesAsync().ReturnsForAnyArgs(telefones);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterTodosTelefonesResponse>>().Which.Value.Telefones.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterTodosTelefonesResponse>>().Which.Value.Telefones.Count().Should().Be(2);
        result.Should().BeOfType<Result<ObterTodosTelefonesResponse>>().Which.Value.Telefones[0].Numero.Should().Be("123456789");
        result.Should().BeOfType<Result<ObterTodosTelefonesResponse>>().Which.Value.Telefones[1].Numero.Should().Be("987654321");
    }

}
