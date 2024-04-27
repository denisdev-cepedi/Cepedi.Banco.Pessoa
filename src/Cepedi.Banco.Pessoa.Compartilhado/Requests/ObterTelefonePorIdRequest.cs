using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterTelefonePorCepIdRequest : IRequest<Result<ObterTelefonePorIdResponse>>
{
    public string Id { get; set; } = default!;
}