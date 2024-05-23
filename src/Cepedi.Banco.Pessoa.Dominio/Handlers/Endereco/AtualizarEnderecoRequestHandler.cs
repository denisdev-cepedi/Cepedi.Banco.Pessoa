using Cepedi.Banco.Pessoa.Compartilhado.Requests;
using Cepedi.Banco.Pessoa.Compartilhado.Responses;
using Cepedi.Banco.Pessoa.Dominio.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Pessoa.Dominio.Handlers;

public class AtualizarEnderecoRequestHandler : IRequestHandler<AtualizarEnderecoRequest, Result<AtualizarEnderecoResponse>>
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AtualizarEnderecoRequestHandler> _logger;
    public AtualizarEnderecoRequestHandler(IEnderecoRepository enderecoRepository, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork, ILogger<AtualizarEnderecoRequestHandler> logger)
    {
        _enderecoRepository = enderecoRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<AtualizarEnderecoResponse>> Handle(AtualizarEnderecoRequest request, CancellationToken cancellationToken)
    {
        var endereco = await _enderecoRepository.ObterEnderecoAsync(request.Id);

        if (endereco is null)
        {
            _logger.LogError("Endereço não encontrado");
            return Result.Error<AtualizarEnderecoResponse>(new Compartilhado.Exceptions.EnderecoNaoEncontradoExcecao());
        }

        var enderecoPrincipal = await _pessoaRepository.ObterEnderecoPrincipalAsync(endereco.IdPessoa);

        if (request.Principal == false && (enderecoPrincipal is null || endereco.Id == enderecoPrincipal.Id))
        {
            _logger.LogError("A pessoa deve ter pelo menos um Endereço principal");
            return Result.Error<AtualizarEnderecoResponse>(new Compartilhado.Exceptions.MinimoUmEnderecoPrincipalException());
        }

        if (enderecoPrincipal is not null && request.Principal == true)
        {
            _logger.LogWarning("Alterando Endereço principal");
            enderecoPrincipal.Principal = false;
            await _enderecoRepository.AtualizarEnderecoAsync(enderecoPrincipal);
        }

        endereco.Atualizar(request);
        await _enderecoRepository.AtualizarEnderecoAsync(endereco);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Endereço atualizado");

        return Result.Success(new AtualizarEnderecoResponse()
        {
            Id = endereco.Id,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Uf = request.Uf,
            Pais = request.Pais,
            Numero = request.Numero,
            Principal = request.Principal
        });
    }
}
