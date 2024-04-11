namespace Cepedi.Banco.Pessoa.Domain.Entities;

public class EnderecoEntity
{
    public int Id { get; set; }
    public string Cep { get; set; } = default!;
    public string Logradouro { get; set; } = default!;
    public string Complemento { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Uf { get; set; } = default!;
    public string Pais { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public int IdPessoa { get; set; }

    public PessoaEntity Pessoa { get; set; } = default!;
}