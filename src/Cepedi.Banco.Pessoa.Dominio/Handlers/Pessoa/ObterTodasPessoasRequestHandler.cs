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
using Cepedi.Banco.Pessoa.Dominio.Services;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterTodasPessoasRequestHandler : IRequestHandler<ObterTodasPessoasRequest, Result<ObterTodasPessoasResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaQueryRepository _pessoaQueryRepository;
        private readonly ICache<ObterTodasPessoasResponse> _cache;
        private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;

        public ObterTodasPessoasRequestHandler(IPessoaRepository pessoaRepository, IPessoaQueryRepository pessoaQueryRepository, ICache<ObterTodasPessoasResponse> cache, ILogger<ObterTodasPessoasRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _pessoaQueryRepository = pessoaQueryRepository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<ObterTodasPessoasResponse>> Handle(ObterTodasPessoasRequest request, CancellationToken cancellationToken)
        {
            var cachePessoas = await _cache.ObterAsync("pessoas");

            if (cachePessoas is not null)
            {
                return Result.Success(cachePessoas);
            }

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
                    Nome = pessoa.Nome,
                    Endereco = new ObterEnderecoResponse
                    {
                        Id = pessoa.EnderecoId,
                        Cep = pessoa.EnderecoCep,
                        Cidade = pessoa.EnderecoCidade,
                        Complemento = pessoa.EnderecoComplemento,
                        Logradouro = pessoa.EnderecoLogradouro,
                        Bairro = pessoa.EnderecoBairro,
                        Uf = pessoa.EnderecoUf,
                        Numero = pessoa.EnderecoNumero,
                        Pais = pessoa.EnderecoPais,
                        Principal = pessoa.EnderecoPrincipal
                    },
                    Telefone = new ObterTelefoneResponse
                    {
                        Id = pessoa.TelefoneId,
                        CodPais = pessoa.TelefoneCodPais,
                        Ddd = pessoa.TelefoneDdd,
                        Numero = pessoa.TelefoneNumero,
                        Tipo = pessoa.TelefoneTipo,
                        Principal = pessoa.TelefonePrincipal
                    }
                }
            ).ToList();

            var response = new ObterTodasPessoasResponse
            {
                Pessoas = pessoas
            };

            await _cache.SalvarAsync("pessoas", response);

            return Result.Success(response);
        }

    }
}
