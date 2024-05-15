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
    public class CadastrarPessoaRequestHandler : IRequestHandler<CadastrarPessoaRequest, Result<CadastrarPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<CadastrarPessoaRequestHandler> _logger;

        public CadastrarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<CadastrarPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<CadastrarPessoaResponse>> Handle(CadastrarPessoaRequest request, CancellationToken cancellationToken)
        {
            
                var novaPessoa = new PessoaEntity
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    DataNascimento = request.DataNascimento,
                    Cpf = request.Cpf,
                    Genero = request.Genero,
                    EstadoCivil = request.EstadoCivil,
                    Nacionalidade = request.Nacionalidade
                };

                var pessoaCadastrada = await _pessoaRepository.CadastrarPessoaAsync(novaPessoa);

                return Result.Success(new CadastrarPessoaResponse { Id = pessoaCadastrada.Id });
            
            
        }
    }
}
