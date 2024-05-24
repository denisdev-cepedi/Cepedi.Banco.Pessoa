using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class ExcluirPessoaRequestValidation : AbstractValidator<ExcluirPessoaRequest>
    {
        public ExcluirPessoaRequestValidation()
        {
            RuleFor(request => request.PessoaId).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
        }
    }
}
