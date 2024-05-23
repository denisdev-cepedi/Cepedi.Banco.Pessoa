using Cepedi.Banco.Pessoa.Dominio.Entidades;

namespace Cepedi.Banco.Pessoa.Dominio.Repository
{
    public interface IPessoaRepository
    {
        Task<PessoaEntity> ObterPessoaAsync(int id);
        Task<PessoaEntity> ObterPessoaPorCpfAsync(string cpf);
        Task<List<EnderecoEntity>> ObterEnderecosPessoaAsync(int id);
        Task<List<TelefoneEntity>> ObterTelefonesPessoaAsync(int id);
        Task<List<PessoaEntity>> ObterTodasPessoasAsync();
        Task<PessoaEntity> CadastrarPessoaAsync(PessoaEntity pessoa);
        Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa);
        Task<PessoaEntity> ExcluirPessoaAsync(PessoaEntity pessoa);
        Task<EnderecoEntity> ObterEnderecoPrincipalAsync(int id);
        Task<TelefoneEntity> ObterTelefonePrincipalAsync(int id);
    }
}
