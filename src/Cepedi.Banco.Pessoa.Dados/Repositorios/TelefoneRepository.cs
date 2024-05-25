using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Banco.Pessoa.Dados.Repositorios;

public class TelefoneRepository : ITelefoneRepository
{
    private readonly ApplicationDbContext _context;

    public TelefoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TelefoneEntity> AtualizarTelefoneAsync(TelefoneEntity telefone)
    {
        _context.Telefone.Update(telefone);

        return telefone;
    }

    public async Task<TelefoneEntity> CadastrarTelefoneAsync(TelefoneEntity telefone)
    {
        _context.Telefone.Add(telefone);

        return telefone;
    }

    public async Task<TelefoneEntity> ExcluirTelefoneAsync(TelefoneEntity telefone)
    {
        _context.Telefone.Remove(telefone);

        return telefone;
    }

    public async Task<TelefoneEntity> ObterTelefoneAsync(int id)
    {
        var telefone = await _context.Telefone.FirstOrDefaultAsync(telefone => telefone.Id == id);
        return telefone;
    }

    public async Task<List<TelefoneEntity>> ObterTodosTelefonesAsync()
    {
        var telefones = await _context.Telefone.ToListAsync();
        return telefones;
    }
    
}
