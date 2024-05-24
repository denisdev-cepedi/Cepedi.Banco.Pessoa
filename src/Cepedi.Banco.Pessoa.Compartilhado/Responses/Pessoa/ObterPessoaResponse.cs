namespace Cepedi.Banco.Pessoa.Compartilhado.Responses
{
    public class ObterPessoaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTimeOffset DataNascimento { get; set; }
        public string Cpf { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public string EstadoCivil { get; set; } = default!;
        public string Nacionalidade { get; set; } = default!;

        public ObterTelefoneResponse? TelefonePrincipal { get; set; }
        public ObterEnderecoResponse? EnderecoPrincipal { get; set; }
    }
}
