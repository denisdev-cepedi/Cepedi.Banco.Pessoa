using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class ObterPessoaRequestValidation : AbstractValidator<ObterPessoaRequest>
    {
        public ObterPessoaRequestValidation()
        {
            RuleFor(request => request.PessoaId).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
        }
    }
}
