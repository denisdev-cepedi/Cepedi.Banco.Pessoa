using Cepedi.Banco.Pessoa.Api.Controllers;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Api.Tests
{
    public class PessoaControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<PessoaController> _logger = Substitute.For<ILogger<PessoaController>>();
        private readonly PessoaController _sut;

        public PessoaControllerTests()
        {
            _sut = new PessoaController(_mediator, _logger);
        }

        [Fact]
        public async Task CadastrarPessoa_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new CadastrarPessoaRequest()
            {
                Cpf = "12345678909",
                Email = "5wzr5@example.com",
                Nome = "Teste",
                DataNascimento = DateTime.Now,
                EstadoCivil = "Solteiro",
                Genero = "Masculino",
                Nacionalidade = "Brasileiro",
            };

            var response = new CadastrarPessoaResponse()
            {
                Cpf = "12345678909",
                Email = "5wzr5@example.com",
                Nome = "Teste",
                DataNascimento = DateTime.Now,
                EstadoCivil = "Solteiro",
                Genero = "Masculino",
                Nacionalidade = "Brasileiro",
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.CadastrarPessoaAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task AtualizarPessoa_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new AtualizarPessoaRequest()
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

            var response = new AtualizarPessoaResponse()
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

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.AtualizarPessoaAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ExcluirPessoa_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ExcluirPessoaRequest()
            {
                PessoaId = 1
            };

            var response = new ExcluirPessoaResponse() { };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ExcluirPessoaAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterPessoa_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterPessoaRequest()
            {
                PessoaId = 1
            };

            var response = new ObterPessoaResponse()
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

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterPessoaAsync(request.PessoaId);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterTodasPessoas_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterTodasPessoasRequest() { };

            var response = new ObterTodasPessoasResponse()
            {
                Pessoas = new List<ObterPessoaResponse>() {
                    new ObterPessoaResponse() {
                        Id = 1,
                        Cpf = "12345678910",
                        Email = "5wzr5@example.com",
                        Nome = "Teste",
                        DataNascimento = DateTime.Now,
                        EstadoCivil = "Solteiro",
                        Genero = "Masculino",
                        Nacionalidade = "Brasileiro",
                    },

                    new ObterPessoaResponse() {
                        Id = 2,
                        Cpf = "12345678909",
                        Email = "5wzr5@example.com",
                        Nome = "Teste",
                        DataNascimento = DateTime.Now,
                        EstadoCivil = "Solteiro",
                        Genero = "Masculino",
                        Nacionalidade = "Brasileiro",
                    },
                }
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterTodasPessoasAsync();

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}
