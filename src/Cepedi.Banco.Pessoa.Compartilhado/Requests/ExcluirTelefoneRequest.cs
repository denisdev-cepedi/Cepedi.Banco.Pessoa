using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ExcluirTelefoneRequest : IRequest<Result<ExcluirTelefoneResponse>>
{
    public int TelefoneId { get; set; }
}