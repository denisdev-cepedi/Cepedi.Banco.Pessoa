using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var telefones = await _pessoaRepository.ObterTelefonesPessoaAsync(request.PessoaId);

            var response = new ObterTelefonesPessoaResponse
            {
                Telefones = telefones.Select(t => new ObterTelefoneResponse { Numero = t.Numero }).ToList()
            };

            return Result.Success(response);
        }
    }
}
