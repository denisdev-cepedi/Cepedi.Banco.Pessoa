using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ExcluirPessoaRequestHandler : IRequestHandler<ExcluirPessoaRequest, Result<ExcluirPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ExcluirPessoaRequestHandler> _logger;

        public ExcluirPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<ExcluirPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ExcluirPessoaResponse>> Handle(ExcluirPessoaRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);
            if (pessoa == null)
            {
                return Result.Error<ExcluirPessoaResponse>(new SemResultadosExcecao());
            }

            await _pessoaRepository.ExcluirPessoaAsync(pessoa);
            return Result.Success(new ExcluirPessoaResponse());
        }

    }
}
