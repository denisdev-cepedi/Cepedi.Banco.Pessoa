using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaPorCpfRequestHandler : IRequestHandler<ObterPessoaPorCpfRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterPessoaPorCpfRequestHandler> _logger;

        public ObterPessoaPorCpfRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterPessoaPorCpfRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaPorCpfRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaPorCpfAsync(request.Cpf);

            if (pessoa is null)
            {
                return Result.Error<ObterPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
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
                Nacionalidade = pessoa.Nacionalidade
            };

            return Result.Success(pessoaResponse);
        }

    }
}
