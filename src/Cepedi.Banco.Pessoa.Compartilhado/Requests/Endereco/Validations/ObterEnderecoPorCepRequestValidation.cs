using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterEnderecoPorCepRequestValidation : AbstractValidator<ObterEnderecoPorCepRequest>
{
    public ObterEnderecoPorCepRequestValidation()
    {
        RuleFor(endereco => endereco.Cep).NotEmpty().WithMessage("O CEP deve ser informado");
        RuleFor(endereco => endereco.Cep).Length(8).WithMessage("O CEP deve possuir 8 dígitos");
    }
}
