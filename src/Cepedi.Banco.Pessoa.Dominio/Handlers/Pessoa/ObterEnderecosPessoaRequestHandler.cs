using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterEnderecosPessoaRequestHandler : IRequestHandler<ObterEnderecosPessoaRequest, Result<ObterEnderecosPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterEnderecosPessoaRequestHandler> _logger;

        public ObterEnderecosPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterEnderecosPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterEnderecosPessoaResponse>> Handle(ObterEnderecosPessoaRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);
            if (pessoa is null)
            {
                _logger.LogError("Pessoa não encontrada");
                return Result.Error<ObterEnderecosPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            var enderecos = await _pessoaRepository.ObterEnderecosPessoaAsync(request.PessoaId);
            if (enderecos == null)
            {
                _logger.LogError("Nenhum Endereço encontrado");
                return Result.Error<ObterEnderecosPessoaResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
            }

            var enderecoResponses = enderecos.Select(e => new ObterEnderecoResponse
            {
                Id = e.Id,
                Cep = e.Cep,
                Logradouro = e.Logradouro,
                Complemento = e.Complemento,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Uf = e.Uf,
                Pais = e.Pais,
                IdPessoa = e.IdPessoa,
                Numero = e.Numero
            }).ToList();

            var response = new ObterEnderecosPessoaResponse
            {
                Enderecos = enderecoResponses
            };

            _logger.LogInformation("Retornando lista de endereços da pessoa");

            return Result.Success(response);
        }

    }
}
