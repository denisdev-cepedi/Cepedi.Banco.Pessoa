using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class CadastrarTelefoneRequest : IRequest<Result<CadastrarTelefoneResponse>>, IValida
{
    public string CodPais { get; set; } = default!;
    public string Ddd { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public int IdPessoa { get; set; }
   

}
