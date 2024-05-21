using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Banco.Pessoa.Dominio.Repository.Queries;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaPorCpfRequestHandler : IRequestHandler<ObterPessoaPorCpfRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaQueryRepository _pessoaQueryRepository;
        private readonly ILogger<ObterPessoaPorCpfRequestHandler> _logger;

        public ObterPessoaPorCpfRequestHandler(IPessoaRepository pessoaRepository, IPessoaQueryRepository pessoaQueryRepository, ILogger<ObterPessoaPorCpfRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _pessoaQueryRepository = pessoaQueryRepository;
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaPorCpfRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaQueryRepository.ObterPessoaPorCpfAsync(request.Cpf);
            if (pessoa == null)
            {
                return Result.Error<ObterPessoaResponse>(new SemResultadosExcecao());
            }

            var pessoaResponse = new ObterPessoaResponse
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
                },
                Telefone = new ObterTelefoneResponse
                {
                    Id = pessoa.TelefoneId,
                    CodPais = pessoa.TelefoneCodPais,
                    Ddd = pessoa.TelefoneDdd,
                    Numero = pessoa.TelefoneNumero,
                    Tipo = pessoa.TelefoneTipo,
                }
            };

            return Result.Success(pessoaResponse);
        }

    }
}
