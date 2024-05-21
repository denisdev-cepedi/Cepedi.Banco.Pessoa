using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class CadastrarTelefoneRequestValidation : AbstractValidator<CadastrarTelefoneRequest>
{
    public CadastrarTelefoneRequestValidation()
    {
        RuleFor(telefone => telefone.CodPais).NotEmpty().WithMessage("O código do país deve ser informado");
        RuleFor(telefone => telefone.CodPais).MaximumLength(3).WithMessage("O Código do país deve possuir até 3 dígitos");
        RuleFor(telefone => telefone.Ddd).NotEmpty().WithMessage("O DDD deve ser informado");
        RuleFor(telefone => telefone.Ddd).MaximumLength(3).WithMessage("O DDD deve possuir até 3 dígitos");
        RuleFor(telefone => telefone.Numero).NotEmpty().WithMessage("O número de telefone deve ser informado");
        RuleFor(telefone => telefone.Numero).Length(9).WithMessage("O número de telefone deve possuir 9 dígitos");
        RuleFor(telefone => telefone.Tipo).NotEmpty().WithMessage("O tipo do telefone deve ser informado");
        RuleFor(telefone => telefone.IdPessoa).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
    }
}
