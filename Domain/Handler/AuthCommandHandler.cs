using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Command;
using Domain.Environments;
using Domain.Model;
using Domain.Model.Response;
using Domain.MongoDB.Collections;
using Domain.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;

namespace Domain.Handler;

public class AuthCommandHandler : IRequestHandler<AuthCommand, ModelResponse>
{
    private readonly IMongoRepository<Users> _repositoryUser;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public AuthCommandHandler(IMongoRepository<Users> repositoryUser, IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore)
    {
        _repositoryUser = repositoryUser;
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
    }

    public async Task<ModelResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}