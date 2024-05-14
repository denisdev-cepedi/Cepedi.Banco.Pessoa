using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterPessoaRequest : IRequest<Result<ObterPessoaResponse>>
{
    public int PessoaId { get; set; }
}
