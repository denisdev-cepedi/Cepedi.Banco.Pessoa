using Cepedi.Banco.Pessoa.Dominio.Repository;

namespace Cepedi.Banco.Pessoa.Dados.Repositorios;
public class Repository<T> :
    IDisposable, IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private bool dispose;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> AdicionarAsync(T entidade, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entidade, cancellationToken);

        return entidade;
    }

    public T Atualizar(T entidade, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entidade);

        return entidade;
    }

    public T Excluir(T entidade, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entidade);

        return entidade;
    }

    private void Dispose(bool disposing)
    {
        if (!dispose && disposing)
        {
            _context?.Dispose();
        }

        dispose = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
