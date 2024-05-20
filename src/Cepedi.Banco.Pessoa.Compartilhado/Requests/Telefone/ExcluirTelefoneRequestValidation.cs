using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class ExcluirTelefoneRequestValidation : AbstractValidator<ExcluirTelefoneRequest>
{
    public ExcluirTelefoneRequestValidation()
    {
        RuleFor(telefone => telefone.TelefoneId).NotEmpty().WithMessage("O ID do telefone deve ser informado");
    }
}
