using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class AtualizarEnderecoRequestValidation : AbstractValidator<AtualizarEnderecoRequest>
{
    public AtualizarEnderecoRequestValidation()
    {
        RuleFor(endereco => endereco.Cep).NotEmpty().WithMessage("O CEP deve ser informado");
        RuleFor(endereco => endereco.Cep).Length(8).WithMessage("O CEP deve possuir 8 dígitos");
        RuleFor(endereco => endereco.Logradouro).NotEmpty().WithMessage("O logradouro deve ser informado");
        RuleFor(endereco => endereco.Bairro).NotEmpty().WithMessage("O bairro deve ser informado");
        RuleFor(endereco => endereco.Cidade).NotEmpty().WithMessage("A cidade deve ser informada");
        RuleFor(endereco => endereco.Uf).NotEmpty().WithMessage("O estado deve ser informado");
        RuleFor(endereco => endereco.Pais).NotEmpty().WithMessage("O país deve ser informado");
        RuleFor(endereco => endereco.Numero).NotEmpty().WithMessage("O número deve ser informado");
        RuleFor(endereco => endereco.IdPessoa).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
    }
}
