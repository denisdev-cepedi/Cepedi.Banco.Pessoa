using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
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

                var pessoaResponses = new List<ObterPessoaResponse>();
                foreach (var pessoa in pessoas)
                {
                    pessoaResponses.Add(new ObterPessoaResponse
                    {
                        Id = pessoa.Id,
                        Nome = pessoa.Nome,
                        Email = pessoa.Email,
                        DataNascimento = pessoa.DataNascimento,
                        Cpf = pessoa.Cpf,
                        Genero = pessoa.Genero,
                        EstadoCivil = pessoa.EstadoCivil,
                        Nacionalidade = pessoa.Nacionalidade

                    });
                }

                var response = new ObterTodasPessoasResponse
                {
                    Pessoas = pessoaResponses
                };

                return Result.Success(response);
            
           
        }
    }
}
