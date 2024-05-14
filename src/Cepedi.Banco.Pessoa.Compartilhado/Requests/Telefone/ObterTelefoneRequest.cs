using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterTelefoneRequest : IRequest<Result<ObterTelefoneResponse>>, IValida
{
    public int TelefoneId { get; set; }
}
