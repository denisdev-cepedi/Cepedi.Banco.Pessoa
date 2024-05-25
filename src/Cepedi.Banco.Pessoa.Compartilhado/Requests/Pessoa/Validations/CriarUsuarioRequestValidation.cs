using FluentValidation;

namespace Cepedi.Compartilhado.Requests
{
    public class CriarUsuarioRequestValidation : AbstractValidator<CriarUsuarioRequest>
    {
        public CriarUsuarioRequestValidation()
        {
            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome do usuário deve ser informado");

            RuleFor(usuario => usuario.DataNascimento).NotEmpty().WithMessage("A data de nascimento do usuário deve ser informada");

            RuleFor(usuario => usuario.Cpf).NotEmpty().WithMessage("O CPF do usuário deve ser informado")
                .Length(11).WithMessage("O CPF deve possuir 11 dígitos");

            RuleFor(usuario => usuario.Celular).NotEmpty().WithMessage("O número de celular do usuário deve ser informado");

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O e-mail do usuário deve ser informado")
                .EmailAddress().WithMessage("Formato de e-mail inválido");
        }
    }
}
