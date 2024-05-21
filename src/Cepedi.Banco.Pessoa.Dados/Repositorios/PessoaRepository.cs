using Dapper;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Threading.Tasks;
using Cepedi.Banco.Pessoa.Dominio;
public class PessoaRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IMemoryCache _cache;

    public PessoaRepository(IDbConnection dbConnection, IMemoryCache cache)
    {
        _dbConnection = dbConnection;
        _cache = cache;
    }

    public async Task<Pessoa> GetPessoaByIdAsync(int id)
    {
        // Define uma chave para o cache
        var cacheKey = $"Pessoa-{id}";

        // Tenta obter o valor do cache
        if (!_cache.TryGetValue(cacheKey, out Pessoa pessoa))
        {
            // Se não estiver no cache, executa a query com Dapper
            pessoa = await _dbConnection.QueryFirstOrDefaultAsync<Pessoa>(
                "SELECT * FROM Pessoa WHERE Id = @Id", new { Id = id });

            // Define opções de cache (por exemplo, expira em 5 minutos)
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            // Armazena o resultado no cache
            _cache.Set(cacheKey, pessoa, cacheOptions);
        }

        return pessoa;
    }
}

public class Pessoa
{
}