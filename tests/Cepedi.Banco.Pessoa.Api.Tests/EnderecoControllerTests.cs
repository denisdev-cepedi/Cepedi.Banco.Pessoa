using Cepedi.Banco.Pessoa.Api.Controllers;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Api.Tests
{
    public class EnderecoControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<EnderecoController> _logger = Substitute.For<ILogger<EnderecoController>>();
        private readonly EnderecoController _sut;

        public EnderecoControllerTests()
        {
            _sut = new EnderecoController(_mediator, _logger);
        }

        [Fact]
        public async Task CadastrarEndereco_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new CadastrarEnderecoRequest()
            {
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123",
                IdPessoa = 1
            };

            var response = new CadastrarEnderecoResponse()
            {
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123"
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.CadastrarEnderecoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task AtualizarEndereco_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new AtualizarEnderecoRequest()
            {
                Id = 1,
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123"
            };

            var response = new AtualizarEnderecoResponse()
            {
                Id = 1,
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123"
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.AtualizarEnderecoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ExcluirEndereco_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ExcluirEnderecoRequest()
            {
                EnderecoId = 1
            };

            var response = new ExcluirEnderecoResponse() { };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ExcluirEnderecoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterEndereco_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterEnderecoRequest()
            {
                EnderecoId = 1
            };

            var response = new ObterEnderecoResponse()
            {
                Id = 1,
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123"
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterEnderecoAsync(request.EnderecoId);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterTodosEnderecos_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterTodosEnderecosRequest() { };

            var response = new ObterTodosEnderecosResponse()
            {
                Enderecos = new List<ObterEnderecoResponse>() {
                    new ObterEnderecoResponse() {
                        Id = 1,
                        Cep = "12345678",
                        Logradouro = "Logradouro",
                        Complemento = "Complemento",
                        Bairro = "Bairro",
                        Cidade = "Cidade",
                        Uf = "UF",
                        Pais = "Pais",
                        Numero = "123"
                    },

                    new ObterEnderecoResponse() {
                        Id = 2,
                        Cep = "12345679",
                        Logradouro = "Logradouro",
                        Complemento = "Complemento",
                        Bairro = "Bairro",
                        Cidade = "Cidade",
                        Uf = "UF",
                        Pais = "Pais",
                        Numero = "321"
                    },
                }
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterTodosEnderecosAsync();

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterEnderecoPorCep_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange 
            var request = new ObterEnderecoPorCepRequest()
            {
                Cep = "12345678"
            };

            var response = new ObterEnderecoPorCepResponse()
            {
                Id = 1,
                Cep = "12345678",
                Logradouro = "Logradouro",
                Complemento = "Complemento",
                Bairro = "Bairro",
                Cidade = "Cidade",
                Uf = "UF",
                Pais = "Pais",
                Numero = "123"
            };

            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(response));

            // Act
            await _sut.ObterEnderecoPorCepAsync(request.Cep);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}
