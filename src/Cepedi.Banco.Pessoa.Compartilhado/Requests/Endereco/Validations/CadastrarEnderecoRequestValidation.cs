using FluentValidation;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests;

public class CadastrarEnderecoRequestValidation : AbstractValidator<CadastrarEnderecoRequest>
{
    public CadastrarEnderecoRequestValidation()
    {
        RuleFor(endereco => endereco.Cep).NotEmpty().WithMessage("O CEP deve ser informado");
        RuleFor(endereco => endereco.Cep).Length(8).WithMessage("O CEP deve possuir 8 dígitos");
        RuleFor(endereco => endereco.Logradouro).NotEmpty().WithMessage("O logradouro deve ser informado");
        RuleFor(endereco => endereco.Bairro).NotEmpty().WithMessage("O bairro deve ser informado");
        RuleFor(endereco => endereco.Cidade).NotEmpty().WithMessage("A cidade deve ser informada");
        RuleFor(endereco => endereco.Uf).NotEmpty().WithMessage("A UF deve ser informada");
        RuleFor(endereco => endereco.Uf).Length(2).WithMessage("A UF deve possuir 2 caracteres");
        RuleFor(endereco => endereco.Pais).NotEmpty().WithMessage("O país deve ser informado");
        RuleFor(endereco => endereco.Numero).NotEmpty().WithMessage("O número deve ser informado");
        RuleFor(endereco => endereco.IdPessoa).NotEmpty().WithMessage("O ID da pessoa deve ser informado");
    }
}
