using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ExcluirEnderecoRequestValidation : AbstractValidator<ExcluirEnderecoRequest>
{
    public ExcluirEnderecoRequestValidation()
    {
        RuleFor(endereco => endereco.EnderecoId).NotEmpty().WithMessage("O ID do endereço deve ser informado");
    }
}
