using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Banco.Pessoa.Dados.Repositorios;

public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Update(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<PessoaEntity> CadastrarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<PessoaEntity> ExcluirPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Remove(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<PessoaEntity> ObterPessoaAsync(int id)
    {
        var pessoa = await _context.Pessoa
            .Include(p => p.Enderecos)
            .Include(p => p.Telefones)
            .FirstOrDefaultAsync(p => p.Id == id);
        return pessoa;
    }

    public async Task<List<EnderecoEntity>> ObterEnderecosPessoaAsync(int id)
    {
        var pessoa = await _context.Pessoa
            .Include(p => p.Enderecos)
            .FirstOrDefaultAsync(p => p.Id == id);
        return pessoa?.Enderecos?.ToList() ?? new List<EnderecoEntity>();
    }

    public async Task<List<TelefoneEntity>> ObterTelefonesPessoaAsync(int id)
    {
        var pessoa = await _context.Pessoa
            .Include(p => p.Telefones)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pessoa == null || pessoa.Telefones == null)
        {
            return new List<TelefoneEntity>();
        }

        return pessoa.Telefones.ToList();
    }


    public async Task<List<PessoaEntity>> ObterTodasPessoasAsync()
    {
        var pessoas = await _context.Pessoa
            .Include(p => p.Enderecos)
            .Include(p => p.Telefones)
            .ToListAsync();
        return pessoas;
    }
}
