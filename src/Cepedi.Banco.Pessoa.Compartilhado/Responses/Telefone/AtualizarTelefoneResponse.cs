namespace Cepedi.Banco.Pessoa.Compartilhado.Responses;

public class AtualizarTelefoneResponse
{
    public int Id { get; set; }
    public string CodPais { get; set; } = default!;
    public string Ddd { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public bool Principal { get; set; } = default!;
}
