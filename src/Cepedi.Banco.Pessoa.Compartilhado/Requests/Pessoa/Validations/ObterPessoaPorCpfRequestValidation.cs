using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class ObterPessoaPorCpfRequestValidation : AbstractValidator<ObterPessoaPorCpfRequest>
    {
        public ObterPessoaPorCpfRequestValidation()
        {
            RuleFor(request => request.Cpf).NotEmpty().WithMessage("O CPF da pessoa deve ser informado")
                .Length(11).WithMessage("O CPF deve possuir 11 dígitos");
        }
    }
}
