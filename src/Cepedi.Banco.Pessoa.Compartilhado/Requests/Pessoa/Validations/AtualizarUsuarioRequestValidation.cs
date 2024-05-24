using FluentValidation;

namespace Cepedi.Compartilhado.Requests
{
    public class AtualizarUsuarioRequestValidation : AbstractValidator<AtualizarUsuarioRequest>
    {
        public AtualizarUsuarioRequestValidation()
        {
            RuleFor(usuario => usuario.Id).NotEmpty().WithMessage("O ID do usuário deve ser informado");
            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome do usuário deve ser informado");
            RuleFor(usuario => usuario.DataNascimento).NotEmpty().WithMessage("A data de nascimento do usuário deve ser informada");
            RuleFor(usuario => usuario.Cpf).NotEmpty().WithMessage("O CPF do usuário deve ser informado");
            RuleFor(usuario => usuario.Celular).NotEmpty().WithMessage("O número de celular do usuário deve ser informado");
            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O e-mail do usuário deve ser informado");
            
        }
    }
}
