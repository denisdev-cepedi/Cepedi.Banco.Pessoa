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
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ILogger<ObterPessoaRequestHandler> _logger;

        public ObterPessoaRequestHandler(
            IPessoaRepository pessoaRepository,
            ITelefoneRepository telefoneRepository,
            IEnderecoRepository enderecoRepository,
            ILogger<ObterPessoaRequestHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _telefoneRepository = telefoneRepository;
            _enderecoRepository = enderecoRepository;
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

            
            var telefonePrincipal = await _telefoneRepository.ObterTelefonePrincipalAsync(pessoa.Id);
            var enderecoPrincipal = await _enderecoRepository.ObterEnderecoPrincipalAsync(pessoa.Id);

            
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

        private CadastrarTelefoneResponse MapToCadastrarTelefoneResponse(TelefoneEntity telefone)
        {
            
            return new CadastrarTelefoneResponse
            {
                CodPais = telefone.CodPais,
                Ddd = telefone.Ddd,
                Numero = telefone.Numero,
                Tipo = telefone.Tipo
                
            };
        }

        private CadastrarEnderecoResponse MapToCadastrarEnderecoResponse(EnderecoEntity endereco)
        {
            
            return new CadastrarEnderecoResponse
            {
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Uf = endereco.Uf,
                Pais = endereco.Pais,
                Numero = endereco.Numero
                
            };
        }
    }
}
