using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class ExclusaoTelefonePrincipalException : AplicacaoExcecao
{
    public ExclusaoTelefonePrincipalException() : base(BancoCentralMensagemErrors.TentativaExclusaoTelefonePrincipal)
    {
    }
}
