using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;

public class MinimoUmEnderecoPrincipalException : AplicacaoExcecao
{
    public MinimoUmEnderecoPrincipalException() : base(BancoCentralMensagemErrors.MinimoUmEnderecoPrincipal)
    {
    }
}
