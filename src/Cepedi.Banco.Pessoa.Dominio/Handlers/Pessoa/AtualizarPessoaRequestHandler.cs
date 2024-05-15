using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class AtualizarPessoaRequestHandler : IRequestHandler<AtualizarPessoaRequest, Result<AtualizarPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<AtualizarPessoaRequestHandler> _logger;

        public AtualizarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<AtualizarPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<AtualizarPessoaResponse>> Handle(AtualizarPessoaRequest request, CancellationToken cancellationToken)
        {
           
                var pessoa = await _pessoaRepository.ObterPessoaAsync(request.Id);

               
                pessoa.Atualizar(request);

                var pessoaAtualizada = await _pessoaRepository.AtualizarPessoaAsync(pessoa);

                var response = new AtualizarPessoaResponse
                {
                    Id = pessoaAtualizada.Id,
                    Nome = pessoaAtualizada.Nome,
                    Email = pessoaAtualizada.Email,
                    DataNascimento = pessoaAtualizada.DataNascimento,
                    Cpf = pessoaAtualizada.Cpf,
                    Genero = pessoaAtualizada.Genero,
                    EstadoCivil = pessoaAtualizada.EstadoCivil,
                    Nacionalidade = pessoaAtualizada.Nacionalidade
                };

                return Result.Success(response);
            
            
        }
    }
}
