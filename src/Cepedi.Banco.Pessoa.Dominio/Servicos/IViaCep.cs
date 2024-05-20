using Cepedi.Banco.Pessoa.Compartilhado.Dtos;
using Refit;

namespace Cepedi.Banco.Pessoa.Dominio.Services;
public interface IViaCep
{
    [Get("/{cep}/json")]
    Task<ApiResponse<ViaCepDTO>> ObterEnderecoPorCep([Body] string cep);
}
