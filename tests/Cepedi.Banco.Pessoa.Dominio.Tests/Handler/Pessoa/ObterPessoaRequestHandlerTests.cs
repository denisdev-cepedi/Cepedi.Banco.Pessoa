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

public class ObterPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<ObterPessoaRequestHandler> _logger = Substitute.For<ILogger<ObterPessoaRequestHandler>>();
    private readonly ObterPessoaRequestHandler _sut;

    public ObterPessoaRequestHandlerTests()
    {
        _sut = new ObterPessoaRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task ObterPessoaAsync_QuandoObter_DeveRetornarSucesso()
    {
        //Arrange 
        var request = new ObterPessoaRequest { PessoaId = 1 };

        _pessoaRepository.ObterTelefonePrincipalAsync(It.IsAny<int>()).ReturnsForAnyArgs(new TelefoneEntity());
        _pessoaRepository.ObterEnderecoPrincipalAsync(It.IsAny<int>()).ReturnsForAnyArgs(new EnderecoEntity());

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

        _pessoaRepository.ObterPessoaAsync(It.IsAny<int>()).ReturnsForAnyArgs(pessoaEntity);

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<ObterPessoaResponse>>().Which.Value.Id.Should().Be(request.PessoaId);
    }

}
