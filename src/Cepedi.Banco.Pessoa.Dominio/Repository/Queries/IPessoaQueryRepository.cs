using Cepedi.Banco.Pessoa.Compartilhado;
using Cepedi.Banco.Pessoa.Dominio.Entidades;

namespace Cepedi.Banco.Pessoa.Dominio.Repository.Queries;

public interface IPessoaQueryRepository
{
    Task<List<PessoaEntity>> ObterPessoasAsync();
    Task<PessoaEntity> ObterPessoaPorCpfAsync(string cpf);
}
