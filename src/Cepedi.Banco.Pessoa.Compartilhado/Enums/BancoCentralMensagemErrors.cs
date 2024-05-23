using Cepedi.Compartilhado.Exceptions;

namespace Cepedi.Banco.Pessoa.Compartilhado.Enums;
public class BancoCentralMensagemErrors
{
    public static ResultadoErro Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro ErroGravacaoUsuario = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro EnderecoNaoEncontrado = new()
    {
        Titulo = "Endereço não encontrado",
        Descricao = "Não foi possível encontrar o endereço informado",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro TelefoneNaoEncontrado = new()
    {
        Titulo = "Telefone não encontrado",
        Descricao = "Não foi possível encontrar o telefone informado",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro PessoaNaoEncontrada = new()
    {
        Titulo = "Pessoa não encontrada",
        Descricao = "Não foi possível encontrar a pessoa informada",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro CpfJaExiste = new()
    {
        Titulo = "CPF já existe",
        Descricao = "Uma pessoa com este CPF já está cadastrada",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro MinimoUmTelefonePrincipal = new()
    {
        Titulo = "Minimo um telefone principal",
        Descricao = "É preciso ter pelo menos um telefone principal",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro MinimoUmEnderecoPrincipal = new()
    {
        Titulo = "Minimo um endereco principal",
        Descricao = "É preciso ter pelo menos um endereco principal",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro TentativaExclusaoTelefonePrincipal = new()
    {
        Titulo = "Tentativa de excluir o telefone principal",
        Descricao = "Não é possível excluir o telefone principal de uma pessoa",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro TentativaExclusaoEnderecoPrincipal = new()
    {
        Titulo = "Tentativa de excluir o endereco principal",
        Descricao = "Não é possível excluir o endereco principal de uma pessoa",
        Tipo = ETipoErro.Erro
    };
}
