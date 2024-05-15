using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;
namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

 public class ObterEnderecosPessoaRequest : IRequest<Result<ObterEnderecosPessoaResponse>>
    {
        public int PessoaId { get; set; }
    }
