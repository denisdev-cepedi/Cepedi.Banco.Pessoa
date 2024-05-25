using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class CadastrarPessoaRequestValidation : AbstractValidator<CadastrarPessoaRequest>
    {
        public CadastrarPessoaRequestValidation()
        {
            RuleFor(pessoa => pessoa.Nome).NotEmpty().WithMessage("O nome da pessoa deve ser informado");

            RuleFor(pessoa => pessoa.Email).NotEmpty().WithMessage("O e-mail da pessoa deve ser informado")
                .EmailAddress().WithMessage("Formato de e-mail inválido");

            RuleFor(pessoa => pessoa.DataNascimento).NotEmpty().WithMessage("A data de nascimento da pessoa deve ser informada");

            RuleFor(pessoa => pessoa.Cpf).NotEmpty().WithMessage("O CPF da pessoa deve ser informado")
                .Length(11).WithMessage("O CPF deve possuir 11 dígitos");

            RuleFor(pessoa => pessoa.Genero).NotEmpty().WithMessage("O gênero da pessoa deve ser informado");

            RuleFor(pessoa => pessoa.EstadoCivil).NotEmpty().WithMessage("O estado civil da pessoa deve ser informado");

            RuleFor(pessoa => pessoa.Nacionalidade).NotEmpty().WithMessage("A nacionalidade da pessoa deve ser informada");

            
        }
    }
}
