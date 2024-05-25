using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class ObterTelefonesPessoaRequestValidation : AbstractValidator<ObterTelefonesPessoaRequest>
    {
        public ObterTelefonesPessoaRequestValidation()
        {
            RuleFor(request => request.PessoaId).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
        }
    }
}
