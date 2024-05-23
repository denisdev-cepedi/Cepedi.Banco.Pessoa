using Cepedi.Banco.Pessoa.Dominio.Entidades;

namespace Cepedi.Banco.Pessoa.Dominio.Repository;

public interface ITelefoneRepository
{
    Task<TelefoneEntity> ObterTelefoneAsync(int id);
    Task<List<TelefoneEntity>> ObterTodosTelefonesAsync();
    Task<TelefoneEntity> CadastrarTelefoneAsync(TelefoneEntity telefone);
    Task<TelefoneEntity> AtualizarTelefoneAsync(TelefoneEntity telefone);
    Task<TelefoneEntity> ExcluirTelefoneAsync(TelefoneEntity telefone);
}
