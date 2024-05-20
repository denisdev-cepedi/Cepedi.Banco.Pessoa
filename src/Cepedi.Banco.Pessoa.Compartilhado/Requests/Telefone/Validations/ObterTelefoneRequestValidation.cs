using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ObterTelefoneRequestValidation : AbstractValidator<ObterTelefoneRequest>
{
    public ObterTelefoneRequestValidation()
    {
        RuleFor(telefone => telefone.TelefoneId).NotEmpty().WithMessage("O ID do telefone deve ser informado");
    }
}
