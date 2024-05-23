using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterPessoaPorCpfRequest : IRequest<Result<ObterPessoaResponse>>
{
    public string Cpf { get; set; } = default!;
}
