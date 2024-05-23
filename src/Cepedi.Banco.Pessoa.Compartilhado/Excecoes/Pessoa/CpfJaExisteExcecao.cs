using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class CpfJaExisteExcecao : AplicacaoExcecao
{
    public CpfJaExisteExcecao() : base(BancoCentralMensagemErrors.CpfJaExiste)
    {
    }
}
