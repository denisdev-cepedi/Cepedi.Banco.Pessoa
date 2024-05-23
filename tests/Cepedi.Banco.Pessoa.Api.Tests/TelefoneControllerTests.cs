using Cepedi.Banco.Pessoa.Api.Controllers;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Api.Tests
{
    public class TelefoneControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<TelefoneController> _logger = Substitute.For<ILogger<TelefoneController>>();
        private readonly TelefoneController _sut;

        public TelefoneControllerTests()
        {
            _sut = new TelefoneController(_mediator, _logger);
        }

        [Fact]
        public async Task CadastrarTelefone_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new CadastrarTelefoneRequest()
            {
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
                IdPessoa = 1
            };

            var response = new CadastrarTelefoneResponse()
            {
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
                IdPessoa = 1
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.CadastrarTelefoneAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task AtualizarTelefone_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new AtualizarTelefoneRequest()
            {
                Id = 1,
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
            };

            var response = new AtualizarTelefoneResponse()
            {
                Id = 1,
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.AtualizarTelefoneAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ExcluirTelefone_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ExcluirTelefoneRequest()
            {
                TelefoneId = 1
            };

            var response = new ExcluirTelefoneResponse() { };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ExcluirTelefoneAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterTelefone_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterTelefoneRequest()
            {
                TelefoneId = 1
            };

            var response = new ObterTelefoneResponse()
            {
                Id = 1,
                CodPais = "55",
                Ddd = "73",
                Numero = "123456789",
                Tipo = "Celular",
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterTelefoneAsync(request.TelefoneId);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterTodosTelefones_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterTodosTelefonesRequest() { };

            var response = new ObterTodosTelefonesResponse()
            {
                Telefones = new List<ObterTelefoneResponse>() {
                    new ObterTelefoneResponse() {
                        Id = 1,
                        CodPais = "55",
                        Ddd = "73",
                        Numero = "123456789",
                        Tipo = "Celular",
                    },

                    new ObterTelefoneResponse() {
                        Id = 2,
                        CodPais = "55",
                        Ddd = "73",
                        Numero = "123456789",
                        Tipo = "Celular",
                    },
                }
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterTodosTelefonesAsync();

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}
