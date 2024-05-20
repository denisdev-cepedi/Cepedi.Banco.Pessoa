namespace Cepedi.Banco.Pessoa.Dominio.Services;
public interface ICache<T>
{
    Task<T> ObterAsync(string chave);

    Task SalvarAsync(string chave, T objeto, int tempoExpiracao = 10);
}