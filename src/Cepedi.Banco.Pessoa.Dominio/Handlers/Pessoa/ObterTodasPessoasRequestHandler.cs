using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Banco.Pessoa.Dominio.Repository.Queries;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterTodasPessoasRequestHandler : IRequestHandler<ObterTodasPessoasRequest, Result<ObterTodasPessoasResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaQueryRepository _pessoaQueryRepository;
        private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;

        public ObterTodasPessoasRequestHandler(IPessoaRepository pessoaRepository, IPessoaQueryRepository pessoaQueryRepository, ILogger<ObterTodasPessoasRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _pessoaQueryRepository = pessoaQueryRepository;
            _logger = logger;
        }

        public async Task<Result<ObterTodasPessoasResponse>> Handle(ObterTodasPessoasRequest request, CancellationToken cancellationToken)
        {
            var pessoaResponse = await _pessoaQueryRepository.ObterPessoasAsync();

            if (pessoaResponse == null || !pessoaResponse.Any())
            {
                return Result.Error<ObterTodasPessoasResponse>(new SemResultadosExcecao());
            }

            var pessoas = pessoaResponse.Select(pessoa =>
                new ObterPessoaResponse
                {
                    Id = pessoa.Id,
                    Cpf = pessoa.Cpf,
                    DataNascimento = pessoa.DataNascimento,
                    Email = pessoa.Email,
                    EstadoCivil = pessoa.EstadoCivil,
                    Genero = pessoa.Genero,
                    Nacionalidade = pessoa.Nacionalidade,
                    Nome = pessoa.Nome
                }
            ).ToList();

            var response = new ObterTodasPessoasResponse
            {
                Pessoas = pessoas
            };

            return Result.Success(response);
        }

    }
}
