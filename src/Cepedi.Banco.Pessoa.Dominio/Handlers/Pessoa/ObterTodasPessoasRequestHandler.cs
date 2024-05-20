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

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterTodasPessoasRequestHandler : IRequestHandler<ObterTodasPessoasRequest, Result<ObterTodasPessoasResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;

        public ObterTodasPessoasRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterTodasPessoasRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterTodasPessoasResponse>> Handle(ObterTodasPessoasRequest request, CancellationToken cancellationToken)
        {
            var pessoas = await _pessoaRepository.ObterTodasPessoasAsync();
            if (pessoas == null || !pessoas.Any())
            {
                return Result.Error<ObterTodasPessoasResponse>(new SemResultadosExcecao());
            }

            var pessoaResponses = pessoas.Select(p => new ObterPessoaResponse
            {
                Id = p.Id,
                Nome = p.Nome,
                Email = p.Email,
                DataNascimento = p.DataNascimento,
                Cpf = p.Cpf,
                Genero = p.Genero,
                EstadoCivil = p.EstadoCivil,
                Nacionalidade = p.Nacionalidade
            }).ToList();

            var response = new ObterTodasPessoasResponse
            {
                Pessoas = pessoaResponses
            };

            return Result.Success(response);
        }

    }
}
