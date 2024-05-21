using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Banco.Pessoa.Dominio.Repository.Queries;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaPorCpfRequestHandler : IRequestHandler<ObterPessoaPorCpfRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaQueryRepository _pessoaQueryRepository;
        private readonly ILogger<ObterPessoaPorCpfRequestHandler> _logger;

        public ObterPessoaPorCpfRequestHandler(IPessoaRepository pessoaRepository, IPessoaQueryRepository pessoaQueryRepository, ILogger<ObterPessoaPorCpfRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _pessoaQueryRepository = pessoaQueryRepository;
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaPorCpfRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaQueryRepository.ObterPessoaPorCpfAsync(request.Cpf);
            if (pessoa == null)
            {
                return Result.Error<ObterPessoaResponse>(new SemResultadosExcecao());
            }

            var pessoaResponse = new ObterPessoaResponse
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Email = pessoa.Email,
                DataNascimento = pessoa.DataNascimento,
                Cpf = pessoa.Cpf,
                Genero = pessoa.Genero,
                EstadoCivil = pessoa.EstadoCivil,
                Nacionalidade = pessoa.Nacionalidade,
            };

            return Result.Success(pessoaResponse);
        }

    }
}
