using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaRequestHandler : IRequestHandler<ObterPessoaRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterPessoaRequestHandler> _logger;

        public ObterPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaRequest request, CancellationToken cancellationToken)
        {
            
                var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);

                

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
