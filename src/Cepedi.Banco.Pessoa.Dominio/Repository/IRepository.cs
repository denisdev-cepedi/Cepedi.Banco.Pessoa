namespace Cepedi.Banco.Pessoa.Dominio.Repository;
public interface IRepository<T> where T : class
{
    Task<T> AdicionarAsync(T entidade, CancellationToken cancellationToken);
    T Atualizar(T entidade, CancellationToken cancellationToken);
    T Excluir(T entidade, CancellationToken cancellationToken);
}
