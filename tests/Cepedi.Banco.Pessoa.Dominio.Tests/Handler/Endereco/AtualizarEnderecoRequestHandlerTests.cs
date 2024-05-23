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

public class AtualizarEnderecoRequestHandlerTests
{
    private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _uinityOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarEnderecoRequestHandler> _logger = Substitute.For<ILogger<AtualizarEnderecoRequestHandler>>();
    private readonly AtualizarEnderecoRequestHandler _sut;

    public AtualizarEnderecoRequestHandlerTests()
    {
        _sut = new AtualizarEnderecoRequestHandler(_enderecoRepository, _pessoaRepository, _uinityOfWork, _logger);
    }

    [Fact]
    public async Task AtualizarEnderecoAsync_QuandoAtualizar_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new AtualizarEnderecoRequest
        {
            Cep = "12345678",
            Logradouro = "Logradouro",
            Complemento = "Complemento",
            Bairro = "Bairro",
            Cidade = "Cidade",
            Uf = "UF",
            Pais = "Pais",
            Numero = "123",
            Principal = true
        };

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

        _pessoaRepository.ObterEnderecoPrincipalAsync(It.IsAny<int>()).ReturnsNullForAnyArgs();
        _enderecoRepository.ObterEnderecoAsync(It.IsAny<int>()).ReturnsForAnyArgs(new EnderecoEntity());
        _enderecoRepository.AtualizarEnderecoAsync(It.IsAny<EnderecoEntity>()).ReturnsForAnyArgs(enderecoEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<AtualizarEnderecoResponse>>().Which.Value.Cep.Should().Be(request.Cep);
        result.Should().BeOfType<Result<AtualizarEnderecoResponse>>().Which.Value.Cep.Should().NotBeEmpty();
    }

}
