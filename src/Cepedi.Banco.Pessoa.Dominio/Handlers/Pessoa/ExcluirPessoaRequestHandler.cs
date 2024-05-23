using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ExcluirPessoaRequestHandler : IRequestHandler<ExcluirPessoaRequest, Result<ExcluirPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExcluirPessoaRequestHandler> _logger;

        public ExcluirPessoaRequestHandler(IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork, ILogger<ExcluirPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<ExcluirPessoaResponse>> Handle(ExcluirPessoaRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);

            if (pessoa is null)
            {
                _logger.LogError("Pessoa não encontrada.");
                return Result.Error<ExcluirPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            await _pessoaRepository.ExcluirPessoaAsync(pessoa);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Pessoa excluida.");

            return Result.Success(new ExcluirPessoaResponse());
        }

    }
}
