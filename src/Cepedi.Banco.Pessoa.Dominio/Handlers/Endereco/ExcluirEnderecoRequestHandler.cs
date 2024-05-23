using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class ExcluirEnderecoRequestHandler : IRequestHandler<ExcluirEnderecoRequest, Result<ExcluirEnderecoResponse>>
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ExcluirEnderecoRequestHandler> _logger;
    public ExcluirEnderecoRequestHandler(IEnderecoRepository enderecoRepository, IPessoaRepository pessoaRepository, ILogger<ExcluirEnderecoRequestHandler> logger)
    {
        _enderecoRepository = enderecoRepository;
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<ExcluirEnderecoResponse>> Handle(ExcluirEnderecoRequest request, CancellationToken cancellationToken)
    {
        var endereco = await _enderecoRepository.ObterEnderecoAsync(request.EnderecoId);

        if (endereco is null)
        {
            return Result.Error<ExcluirEnderecoResponse>(new Compartilhado.Exceptions.EnderecoNaoEncontradoExcecao());
        }

        var enderecoPrincipal = await _pessoaRepository.ObterEnderecoPrincipalAsync(endereco.IdPessoa);

        if (enderecoPrincipal is not null && request.EnderecoId == enderecoPrincipal.Id)
        {
            return Result.Error<ExcluirEnderecoResponse>(new Compartilhado.Exceptions.ExclusaoEnderecoPrincipalException());
        }

        await _enderecoRepository.ExcluirEnderecoAsync(endereco);

        return Result.Success(new ExcluirEnderecoResponse());
    }
}
