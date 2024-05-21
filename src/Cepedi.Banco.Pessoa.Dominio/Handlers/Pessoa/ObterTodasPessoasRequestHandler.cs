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
                        Id = pessoa.Enderecos.First().Id,
                        Cep = pessoa.Enderecos.First().Cep,
                        Cidade = pessoa.Enderecos.First().Cidade,
                        Complemento = pessoa.Enderecos.First().Complemento,
                        Logradouro = pessoa.Enderecos.First().Logradouro,
                        Bairro = pessoa.Enderecos.First().Bairro,
                        Uf = pessoa.Enderecos.First().Uf,
                        Numero = pessoa.Enderecos.First().Numero,
                        Pais = pessoa.Enderecos.First().Pais,
                        Principal = pessoa.Enderecos.First().Principal
                    },
                    Telefone = new ObterTelefoneResponse
                    {
                        Id = pessoa.Telefones.First().Id,
                        CodPais = pessoa.Telefones.First().CodPais,
                        Ddd = pessoa.Telefones.First().Ddd,
                        Numero = pessoa.Telefones.First().Numero,
                        Tipo = pessoa.Telefones.First().Tipo,
                        Principal = pessoa.Telefones.First().Principal
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
