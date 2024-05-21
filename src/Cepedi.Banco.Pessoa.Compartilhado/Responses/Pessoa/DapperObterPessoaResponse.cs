namespace Cepedi.Banco.Pessoa.Compartilhado;

public class DapperObterPessoaResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTimeOffset DataNascimento { get; set; }
    public string Cpf { get; set; } = default!;
    public string Genero { get; set; } = default!;
    public string EstadoCivil { get; set; } = default!;
    public string Nacionalidade { get; set; } = default!;
    public int EnderecoId { get; set; } = default!;
    public string EnderecoCep { get; set; } = default!;
    public string EnderecoLogradouro { get; set; } = default!;
    public string EnderecoComplemento { get; set; } = default!;
    public string EnderecoBairro { get; set; } = default!;
    public string EnderecoCidade { get; set; } = default!;
    public string EnderecoUf { get; set; } = default!;
    public string EnderecoPais { get; set; } = default!;
    public string EnderecoNumero { get; set; } = default!;
    public bool EnderecoPrincipal { get; set; } = default!;
    public int TelefoneId { get; set; } = default!;
    public string TelefoneCodPais { get; set; } = default!;
    public string TelefoneDdd { get; set; } = default!;
    public string TelefoneNumero { get; set; } = default!;
    public string TelefoneTipo { get; set; } = default!;
    public bool TelefonePrincipal { get; set; } = default!;
}
