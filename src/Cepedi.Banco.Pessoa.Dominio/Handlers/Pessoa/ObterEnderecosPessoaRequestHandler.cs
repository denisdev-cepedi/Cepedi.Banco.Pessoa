using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
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
    public class ObterEnderecosPessoaRequestHandler : IRequestHandler<ObterEnderecosPessoaRequest, Result<ObterEnderecosPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterEnderecosPessoaRequestHandler> _logger;

        public ObterEnderecosPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterEnderecosPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterEnderecosPessoaResponse>> Handle(ObterEnderecosPessoaRequest request, CancellationToken cancellationToken)
        {
            
                var enderecos = await _pessoaRepository.ObterEnderecosPessoaAsync(request.PessoaId);

                var enderecoResponses = new List<ObterEnderecoResponse>();
                foreach (var endereco in enderecos)
                {
                    enderecoResponses.Add(new ObterEnderecoResponse
                    {
                        Id = endereco.Id,
                        Cep = endereco.Cep,
                        Logradouro = endereco.Logradouro,
                        Complemento = endereco.Complemento,
                        Bairro = endereco.Bairro,
                        Cidade = endereco.Cidade,
                        Uf = endereco.Uf,
                        Pais = endereco.Pais,
                        Numero = endereco.Numero
                    
                    });
                }

                var response = new ObterEnderecosPessoaResponse
                {
                    Enderecos = enderecoResponses
                };

                return Result.Success(response);
            
           
        }
    }
}
