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

    public async Task<PessoaEntity> ObterPessoaPorCpfAsync(string cpf)
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
                        Endereco.Cep,
                        Endereco.Logradouro,
                        Endereco.Complemento,
                        Endereco.Bairro,
                        Endereco.Cidade,
                        Endereco.Uf,
                        Endereco.Pais,
                        Endereco.Numero,
                        Telefone.CodPais,
                        Telefone.Ddd,
                        Telefone.Numero,
                        Telefone.Tipo
                    FROM
                        Pessoa
                    LEFT JOIN
                        Endereco ON Endereco.IdPessoa = Pessoa.Id AND Endereco.Principal = 1
                    LEFT JOIN
                        Telefone ON Telefone.IdPessoa = Pessoa.Id AND Telefone.Principal = 1
                    Where
                        Pessoa.Cpf = @Cpf";

        var retorno = await ExecuteQueryAsync<PessoaEntity>(sql, parametros);

        return retorno.FirstOrDefault();
    }

    public async Task<List<PessoaEntity>> ObterPessoasAsync()
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
                        Endereco.Cep,
                        Endereco.Logradouro,
                        Endereco.Complemento,
                        Endereco.Bairro,
                        Endereco.Cidade,
                        Endereco.Uf,
                        Endereco.Pais,
                        Endereco.Numero,
                        Telefone.CodPais,
                        Telefone.Ddd,
                        Telefone.Numero,
                        Telefone.Tipo
                    FROM
                        Pessoa
                    LEFT JOIN
                        Endereco ON Endereco.IdPessoa = Pessoa.Id AND Endereco.Principal = 1
                    LEFT JOIN
                        Telefone ON Telefone.IdPessoa = Pessoa.Id AND Telefone.Principal = 1;";

        var retorno = await ExecuteQueryAsync<PessoaEntity>(sql, new DynamicParameters());

        return retorno.ToList();
    }
}
