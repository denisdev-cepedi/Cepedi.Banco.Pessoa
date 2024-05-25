using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class ObterEnderecosPessoaRequestValidation : AbstractValidator<ObterEnderecosPessoaRequest>
    {
        public ObterEnderecosPessoaRequestValidation()
        {
            RuleFor(request => request.PessoaId).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
        }
    }
}
