using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ExcluirPessoaRequest : IRequest<Result<ExcluirPessoaResponse>>
{
    public int PessoaId { get; set; }
}
