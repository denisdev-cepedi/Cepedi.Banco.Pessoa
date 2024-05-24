using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using MediatR;
using OperationResult;
using System;
using System.Collections.Generic;

namespace Cepedi.Banco.Pessoa.Compartilhado.Requests
{
    public class CadastrarPessoaRequest : IRequest<Result<CadastrarPessoaResponse>>
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTimeOffset DataNascimento { get; set; }
        public string Cpf { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public string EstadoCivil { get; set; } = default!;
        public string Nacionalidade { get; set; } = default!;

        public CadastrarTelefonePessoaRequest Telefone { get; set; }
        public CadastrarEnderecoPessoaRequest Endereco { get; set; }

    }
}
