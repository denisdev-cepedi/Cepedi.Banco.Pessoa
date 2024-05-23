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

public class ExcluirEnderecoPorCepRequestHandlerTests
{
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<ExcluirEnderecoRequestHandler> _logger = Substitute.For<ILogger<ExcluirEnderecoRequestHandler>>();
    private readonly ExcluirEnderecoRequestHandler _sut;

    public ExcluirEnderecoPorCepRequestHandlerTests()
    {
        _sut = new ExcluirEnderecoRequestHandler(_enderecoRepository, _pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task ExcluirEnderecoAsync_QuandoExcluir_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ExcluirEnderecoRequest { EnderecoId = 1 };

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

        _enderecoRepository.ObterEnderecoAsync(request.EnderecoId).ReturnsForAnyArgs(enderecoEntity);
        _pessoaRepository.ObterEnderecoPrincipalAsync(request.EnderecoId).ReturnsForAnyArgs(new EnderecoEntity() { Id = 2 });
        _enderecoRepository.ExcluirEnderecoAsync(It.IsAny<EnderecoEntity>()).ReturnsForAnyArgs(enderecoEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ExcluirEnderecoResponse>>().Which.Value.Should().NotBeNull();
    }

}
