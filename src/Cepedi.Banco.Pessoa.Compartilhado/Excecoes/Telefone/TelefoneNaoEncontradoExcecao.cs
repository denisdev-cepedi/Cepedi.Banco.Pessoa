using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class TelefoneNaoEncontradoExcecao : AplicacaoExcecao
{
    public TelefoneNaoEncontradoExcecao() : base(BancoCentralMensagemErrors.TelefoneNaoEncontrado)
    {
    }
}
