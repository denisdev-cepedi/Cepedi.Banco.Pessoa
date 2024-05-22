using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repositorio.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Banco.Pessoa.Dados.Repositorios.Queries;
public class PessoaQueryRepository : BaseDapperRepository, IPessoaQueryRepository
{
    public PessoaQueryRepository(IConfiguration configuration) 
        : base(configuration)
    {
    }

    public async Task<List<PessoaEntity>> ObterPessoasAsync(string nome)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@Nome", nome, System.Data.DbType.String);

        var sql = @"SELECT 
                        Id, 
                        Nome,
                        Email,
                        DataNascimento,
                        Cpf,
                        Genero,
                        EstadoCivil,
                        Nacionalidade
                    FROM Pessoa WITH(NOLOCK)
                    Where
                        Nome = @Nome";

        var retorno = await ExecuteQueryAsync
            <PessoaEntity>(sql, parametros);

        return retorno.ToList();
    }
}
