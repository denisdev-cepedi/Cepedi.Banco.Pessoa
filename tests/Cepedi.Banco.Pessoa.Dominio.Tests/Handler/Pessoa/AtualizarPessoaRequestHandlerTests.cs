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

public class AtualizarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarPessoaRequestHandler> _logger = Substitute.For<ILogger<AtualizarPessoaRequestHandler>>();
    private readonly AtualizarPessoaRequestHandler _sut;

    public AtualizarPessoaRequestHandlerTests()
    {
        _sut = new AtualizarPessoaRequestHandler(_pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task AtualizarPessoaAsync_QuandoAtualizar_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new AtualizarPessoaRequest
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

        var pessoaEntity = new PessoaEntity
        {
            Id = 1,
            Cpf = "12345678909",
            Email = "5wzr5@example.com",
            Nome = "Pessoa Atualizada",
            DataNascimento = DateTime.Now,
            EstadoCivil = "Solteiro",
            Genero = "Masculino",
            Nacionalidade = "Brasileiro",
        };

        _pessoaRepository.ObterPessoaAsync(It.IsAny<int>()).ReturnsForAnyArgs(new PessoaEntity());
        _pessoaRepository.ObterPessoaPorCpfAsync(request.Cpf).ReturnsNullForAnyArgs();
        _pessoaRepository.AtualizarPessoaAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(pessoaEntity);
        _uinityOfWork.SaveChangesAsync().ReturnsForAnyArgs(true);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which.Value.Cpf.Should().Be(request.Cpf);
        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which.Value.Cpf.Should().NotBeEmpty();
    }

}
