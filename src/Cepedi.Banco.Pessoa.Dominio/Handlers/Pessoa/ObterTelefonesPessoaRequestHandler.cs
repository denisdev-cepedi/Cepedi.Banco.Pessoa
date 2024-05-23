using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterTelefonesPessoaRequestHandler : IRequestHandler<ObterTelefonesPessoaRequest, Result<ObterTelefonesPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterTelefonesPessoaRequestHandler> _logger;

        public ObterTelefonesPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterTelefonesPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterTelefonesPessoaResponse>> Handle(ObterTelefonesPessoaRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);
            if (pessoa is null)
            {
                return Result.Error<ObterTelefonesPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            var telefones = await _pessoaRepository.ObterTelefonesPessoaAsync(request.PessoaId);
            if (telefones is null)
            {
                return Result.Error<ObterTelefonesPessoaResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
            }

            var response = new ObterTelefonesPessoaResponse
            {
                Telefones = telefones.Select(t => new ObterTelefoneResponse { Numero = t.Numero }).ToList()
            };

            return Result.Success(response);
        }

    }
}
