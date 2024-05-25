using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterTodasPessoasRequestHandler : IRequestHandler<ObterTodasPessoasRequest, Result<ObterTodasPessoasResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
   
        private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;

        public ObterTodasPessoasRequestHandler(
            IPessoaRepository pessoaRepository, 
            
            ILogger<ObterTodasPessoasRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterTodasPessoasResponse>> Handle(ObterTodasPessoasRequest request, CancellationToken cancellationToken)
        {
            var pessoas = await _pessoaRepository.ObterTodasPessoasAsync();
            if (pessoas == null)
            {
                _logger.LogError("Nenhuma pessoa encontrada");
                return Result.Error<ObterTodasPessoasResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
            }

            var pessoaResponses = new List<ObterPessoaResponse>();

            foreach (var pessoa in pessoas)
            {
                var enderecoPrincipal = await _pessoaRepository.ObterEnderecoPrincipalAsync(pessoa.Id);
                var telefonePrincipal = await _pessoaRepository.ObterTelefonePrincipalAsync(pessoa.Id);

                pessoaResponses.Add(new ObterPessoaResponse
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Email = pessoa.Email,
                    DataNascimento = pessoa.DataNascimento,
                    Cpf = pessoa.Cpf,
                    Genero = pessoa.Genero,
                    EstadoCivil = pessoa.EstadoCivil,
                    Nacionalidade = pessoa.Nacionalidade,
                    EnderecoPrincipal = enderecoPrincipal != null ? new ObterEnderecoResponse
                    {
                        Id = enderecoPrincipal.Id,
                        Cep = enderecoPrincipal.Cep,
                        Logradouro = enderecoPrincipal.Logradouro,
                        Complemento = enderecoPrincipal.Complemento,
                        Bairro = enderecoPrincipal.Bairro,
                        Cidade = enderecoPrincipal.Cidade,
                        Uf = enderecoPrincipal.Uf,
                        Pais = enderecoPrincipal.Pais,
                        Numero = enderecoPrincipal.Numero,
                        Principal = enderecoPrincipal.Principal,
                        IdPessoa = enderecoPrincipal.IdPessoa
                    } : null,
                    TelefonePrincipal = telefonePrincipal != null ? new ObterTelefoneResponse
                    {
                        Id = telefonePrincipal.Id,
                        CodPais = telefonePrincipal.CodPais,
                        Ddd = telefonePrincipal.Ddd,
                        Numero = telefonePrincipal.Numero,
                        Tipo = telefonePrincipal.Tipo,
                        Principal = telefonePrincipal.Principal,
                        IdPessoa = telefonePrincipal.IdPessoa
                    } : null
                });
            }

            var response = new ObterTodasPessoasResponse
            {
                Pessoas = pessoaResponses
            };

            _logger.LogInformation("Retornando lista de pessoas com dados principais");

            return Result.Success(response);
        }
    }
}
