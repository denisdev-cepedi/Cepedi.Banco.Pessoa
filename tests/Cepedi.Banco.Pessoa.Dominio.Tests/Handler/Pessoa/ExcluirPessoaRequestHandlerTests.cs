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

public class ExcluirPessoaPorCepRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<ExcluirPessoaRequestHandler> _logger = Substitute.For<ILogger<ExcluirPessoaRequestHandler>>();
    private readonly ExcluirPessoaRequestHandler _sut;

    public ExcluirPessoaPorCepRequestHandlerTests()
    {
        _sut = new ExcluirPessoaRequestHandler(_pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task ExcluirPessoaAsync_QuandoExcluir_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ExcluirPessoaRequest { PessoaId = 1 };

        var pessoaEntity = new PessoaEntity
        {
            Id = 1,
            Cpf = "12345678909",
            Email = "5wzr5@example.com",
            Nome = "Teste",
            DataNascimento = DateTime.Now,
            EstadoCivil = "Solteiro",
            Genero = "Masculino",
            Nacionalidade = "Brasileiro",
        };

        _pessoaRepository.ObterPessoaAsync(request.PessoaId).ReturnsForAnyArgs(pessoaEntity);
        _pessoaRepository.ExcluirPessoaAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(pessoaEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ExcluirPessoaResponse>>().Which.Value.Should().NotBeNull();
    }

}
