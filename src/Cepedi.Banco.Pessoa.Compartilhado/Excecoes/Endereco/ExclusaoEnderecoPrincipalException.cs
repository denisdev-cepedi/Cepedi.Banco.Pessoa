using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class ExclusaoEnderecoPrincipalException : AplicacaoExcecao
{
    public ExclusaoEnderecoPrincipalException() : base(BancoCentralMensagemErrors.TentativaExclusaoEnderecoPrincipal)
    {
    }
}
