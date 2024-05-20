using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterTodasPessoasRequest : IRequest<Result<ObterTodasPessoasResponse>>
{

}
