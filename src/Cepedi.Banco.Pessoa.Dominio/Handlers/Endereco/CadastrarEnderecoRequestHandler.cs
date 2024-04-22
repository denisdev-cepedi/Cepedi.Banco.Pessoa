using System.Text.Json;
using Cepedi.Banco.Pessoa.Compartilhado.Dtos;
using Cepedi.Banco.Pessoa.Compartilhado.Enums;
using Cepedi.Banco.Pessoa.Compartilhado.Exceptions;
using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using Cepedi.Banco.Pessoa.Dominio.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class CadastrarEnderecoRequestHandler : IRequestHandler<CadastrarEnderecoRequest, Result<CadastrarEnderecoResponse>>
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly ILogger<CadastrarEnderecoRequestHandler> _logger;
    private readonly IViaCep _viaCepApi;
    public CadastrarEnderecoRequestHandler(IEnderecoRepository enderecoRepository, ILogger<CadastrarEnderecoRequestHandler> logger, IViaCep viaCepApi)
    {
        _enderecoRepository = enderecoRepository;
        _logger = logger;
        _viaCepApi = viaCepApi;
    }
    public async Task<Result<CadastrarEnderecoResponse>> Handle(CadastrarEnderecoRequest request, CancellationToken cancellationToken)
    {
        var endereco = new EnderecoEntity()
        {
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Uf = request.Uf,
            Pais = request.Pais,
            Numero = request.Numero,
            IdPessoa = request.IdPessoa
        };

        //fazer consulta a base viacep
        var cepResponse = await _viaCepApi.ObterEnderecoPorCep(request.Cep);
        
        if(cepResponse != null && cepResponse.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return Result.Error<CadastrarEnderecoResponse>(
                new AplicacaoExcecao(BancoCentralMensagemErrors.CepInvalido));
        }

        var enderecoDto = cepResponse.Content;

        await _enderecoRepository.CadastrarEnderecoAsync(endereco);

        return Result.Success(new CadastrarEnderecoResponse()
        {
            Id = endereco.Id,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Uf = request.Uf,
            Pais = request.Pais,
            Numero = request.Numero
        });
    }
}
