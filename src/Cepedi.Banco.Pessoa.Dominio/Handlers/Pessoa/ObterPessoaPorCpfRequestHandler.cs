using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Banco.Pessoa.Dominio.Entidades;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaPorCpfRequestHandler : IRequestHandler<ObterPessoaPorCpfRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterPessoaPorCpfRequestHandler> _logger;

        public ObterPessoaPorCpfRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterPessoaPorCpfRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaPorCpfRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaPorCpfAsync(request.Cpf);

            if (pessoa is null)
            {
                _logger.LogError("Pessoa não encontrada");
                return Result.Error<ObterPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

              
            var telefonePrincipal = await _pessoaRepository.ObterTelefonePrincipalAsync(pessoa.Id);
            var enderecoPrincipal = await _pessoaRepository.ObterEnderecoPrincipalAsync(pessoa.Id);

            
            var telefonePrincipalResponse = MapToCadastrarTelefoneResponse(telefonePrincipal);
            var enderecoPrincipalResponse = MapToCadastrarEnderecoResponse(enderecoPrincipal);

            var pessoaResponse = new ObterPessoaResponse
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Email = pessoa.Email,
                DataNascimento = pessoa.DataNascimento,
                Cpf = pessoa.Cpf,
                Genero = pessoa.Genero,
                EstadoCivil = pessoa.EstadoCivil,
                Nacionalidade = pessoa.Nacionalidade,
                EnderecoPrincipal = enderecoPrincipalResponse,
                TelefonePrincipal = telefonePrincipalResponse
            };

            _logger.LogInformation("Retornando Pessoa encontrada");
            return Result.Success(pessoaResponse);
        }

        private ObterTelefoneResponse MapToCadastrarTelefoneResponse(TelefoneEntity telefone)
        {
            return new ObterTelefoneResponse
            {   Id = telefone.Id,
                CodPais = telefone.CodPais,
                Ddd = telefone.Ddd,
                Numero = telefone.Numero,
                Tipo = telefone.Tipo,
                IdPessoa = telefone.IdPessoa
            };
        }

        private ObterEnderecoResponse MapToCadastrarEnderecoResponse(EnderecoEntity endereco)
        {
            return new ObterEnderecoResponse
            {   Id = endereco.Id,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Uf = endereco.Uf,
                Pais = endereco.Pais,
                Numero = endereco.Numero,
                IdPessoa = endereco.IdPessoa
            };
        }
    }
}
