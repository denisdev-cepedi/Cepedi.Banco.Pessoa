using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using Cepedi.Banco.Pessoa.Compartilhado.Responses; 
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers
{
    public class ObterPessoaRequestHandler : IRequestHandler<ObterPessoaRequest, Result<ObterPessoaResponse>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ObterPessoaRequestHandler> _logger;

        public ObterPessoaRequestHandler(
            IPessoaRepository pessoaRepository,
           
            ILogger<ObterPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
           
            _logger = logger;
        }

        public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaRequest request, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);
            if (pessoa is null)
            {
                _logger.LogError("Pessoa não encontrada.");
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
                TelefonePrincipal = telefonePrincipalResponse,
                EnderecoPrincipal = enderecoPrincipalResponse
            };

            _logger.LogInformation("Retornando Pessoa encontrada.");

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
