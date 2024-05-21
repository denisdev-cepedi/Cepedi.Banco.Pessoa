using Cepedi.Banco.Pessoa.Compartilhado;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Banco.Pessoa.Dados.Repositorios.Queries;
public class PessoaQueryRepository : BaseDapperRepository, IPessoaQueryRepository
{
    public PessoaQueryRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<DapperObterPessoaResponse> ObterPessoaPorCpfAsync(string cpf)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@Cpf", cpf, System.Data.DbType.String);

        var sql = @"SELECT
                        Pessoa.Id,
                        Pessoa.Nome,
                        Pessoa.Email,
                        Pessoa.Cpf,
                        Pessoa.DataNascimento,
                        Pessoa.Genero,
                        Pessoa.EstadoCivil,
                        Pessoa.Nacionalidade,
                        Endereco.Id AS EnderecoId,
                        Endereco.Cep AS EnderecoCep,
                        Endereco.Logradouro AS EnderecoLogradouro,
                        Endereco.Complemento AS EnderecoComplemento,
                        Endereco.Bairro AS EnderecoBairro,
                        Endereco.Cidade AS EnderecoCidade,
                        Endereco.Uf AS EnderecoUf,
                        Endereco.Pais AS EnderecoPais,
                        Endereco.Numero AS EnderecoNumero,
                        Endereco.Principal AS EnderecoPrincipal,
                        Telefone.Id AS TelefoneId,
                        Telefone.CodPais AS TelefoneCodPais,
                        Telefone.Ddd AS TelefoneDdd,
                        Telefone.Numero AS TelefoneNumero,
                        Telefone.Tipo AS TelefoneTipo,
                        Telefone.Principal AS TelefonePrincipal
                    FROM
                        Pessoa
                    LEFT JOIN
                        Endereco ON Endereco.IdPessoa = Pessoa.Id AND Endereco.Principal = 1
                    LEFT JOIN
                        Telefone ON Telefone.IdPessoa = Pessoa.Id AND Telefone.Principal = 1
                    Where
                        Pessoa.Cpf = @Cpf";

        var retorno = await ExecuteQueryAsync<DapperObterPessoaResponse>(sql, parametros);

        return retorno.FirstOrDefault();
    }

    public async Task<List<DapperObterPessoaResponse>> ObterPessoasAsync()
    {
        var sql = @"SELECT
                        Pessoa.Id,
                        Pessoa.Nome,
                        Pessoa.Email,
                        Pessoa.Cpf,
                        Pessoa.DataNascimento,
                        Pessoa.Genero,
                        Pessoa.EstadoCivil,
                        Pessoa.Nacionalidade,
                        Endereco.Id AS EnderecoId,
                        Endereco.Cep AS EnderecoCep,
                        Endereco.Logradouro AS EnderecoLogradouro,
                        Endereco.Complemento AS EnderecoComplemento,
                        Endereco.Bairro AS EnderecoBairro,
                        Endereco.Cidade AS EnderecoCidade,
                        Endereco.Uf AS EnderecoUf,
                        Endereco.Pais AS EnderecoPais,
                        Endereco.Numero AS EnderecoNumero,
                        Endereco.Principal AS EnderecoPrincipal,
                        Telefone.Id AS TelefoneId,
                        Telefone.CodPais AS TelefoneCodPais,
                        Telefone.Ddd AS TelefoneDdd,
                        Telefone.Numero AS TelefoneNumero,
                        Telefone.Tipo AS TelefoneTipo,
                        Telefone.Principal AS TelefonePrincipal
                    FROM
                        Pessoa
                    LEFT JOIN
                        Endereco ON Endereco.IdPessoa = Pessoa.Id AND Endereco.Principal = 1
                    LEFT JOIN
                        Telefone ON Telefone.IdPessoa = Pessoa.Id AND Telefone.Principal = 1;";

        var retorno = await ExecuteQueryAsync<DapperObterPessoaResponse>(sql, new DynamicParameters());

        return retorno.ToList();
    }
}
