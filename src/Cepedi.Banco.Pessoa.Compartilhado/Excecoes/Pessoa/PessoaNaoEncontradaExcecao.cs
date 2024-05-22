using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class PessoaNaoEncontradaExcecao : AplicacaoExcecao
{
    public PessoaNaoEncontradaExcecao() : base(BancoCentralMensagemErrors.PessoaNaoEncontrada)
    {
    }
}
