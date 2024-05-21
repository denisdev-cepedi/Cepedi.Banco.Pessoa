using Cepedi.Banco.Pessoa.Compartilhado;

namespace Cepedi.Banco.Pessoa.Dominio.Repository.Queries;

public interface IPessoaQueryRepository
{
    Task<List<DapperObterPessoaResponse>> ObterPessoasAsync();
    Task<DapperObterPessoaResponse> ObterPessoaPorCpfAsync(string cpf);
}
