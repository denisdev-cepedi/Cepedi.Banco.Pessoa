using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterEnderecoRequestValidation : AbstractValidator<ObterEnderecoRequest>
{
    public ObterEnderecoRequestValidation()
    {
        RuleFor(endereco => endereco.EnderecoId).NotEmpty().WithMessage("O ID do endereço deve ser informado");
    }
}
