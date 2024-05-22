using Cepedi.Banco.Pessoa.Dominio.Entidades;

namespace Cepedi.Banco.Pessoa.Dominio.Repositorio.Queries;
public interface IPessoaQueryRepository
{
    Task<List<PessoaEntity>> ObterPessoasAsync(string nome);
}
