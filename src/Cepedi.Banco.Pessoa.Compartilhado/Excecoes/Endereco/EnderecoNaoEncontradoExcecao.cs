using Cepedi.Banco.Pessoa.Compartilhado.Enums;

namespace Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
public class EnderecoNaoEncontradoExcecao : AplicacaoExcecao
{
    public EnderecoNaoEncontradoExcecao() : base(BancoCentralMensagemErrors.EnderecoNaoEncontrado)
    {
    }
}
