using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;

public class MinimoUmTelefonePrincipalException : AplicacaoExcecao
{
    public MinimoUmTelefonePrincipalException() : base(BancoCentralMensagemErrors.MinimoUmTelefonePrincipal)
    {
    }
}
