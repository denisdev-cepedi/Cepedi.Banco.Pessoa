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
                _logger.LogError("Pessoa não encontrada");
                return Result.Error<ObterTelefonesPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            var telefones = await _pessoaRepository.ObterTelefonesPessoaAsync(request.PessoaId);
            if (telefones is null)
            {
                _logger.LogError("Nenhum Telefone encontrado");
                return Result.Error<ObterTelefonesPessoaResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
            }

            var response = new ObterTelefonesPessoaResponse
            {
                Telefones = telefones.Select(t => new ObterTelefoneResponse
                {
                    Id = t.Id,
                    CodPais = t.CodPais,
                    Ddd = t.Ddd,
                    Numero = t.Numero,
                    Tipo = t.Tipo,
                    Principal = t.Principal,
                    IdPessoa = t.IdPessoa

                }).ToList()
            };

            _logger.LogInformation("Retornando Telefones encontrados");

            return Result.Success(response);
        }

    }
}
