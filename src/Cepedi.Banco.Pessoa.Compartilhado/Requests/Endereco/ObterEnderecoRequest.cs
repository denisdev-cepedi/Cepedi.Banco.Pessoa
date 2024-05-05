using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterEnderecoRequest : IRequest<Result<ObterEnderecoResponse>>, IValida
{
    public int EnderecoId { get; set; }
}
