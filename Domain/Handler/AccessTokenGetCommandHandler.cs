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

public class AccessTokenGetCommandHandler : IRequestHandler<AccessTokenGetCommand, ModelResponse>
{
    private readonly IMongoRepository<Users> _repositoryUser;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public AccessTokenGetCommandHandler(IMongoRepository<Users> repositoryUser, IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore)
    {
        _repositoryUser = repositoryUser;
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
    }

    public async Task<ModelResponse> Handle(AccessTokenGetCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            var check = _repositoryUser.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred)
                .AsQueryable()
                .Where(x => x.Username == request.AccessTokenGetRequest.Username &&
                            x.Password == request.AccessTokenGetRequest.Password).FirstOrDefault();

            if (check != null)
            {
                var issuer = _environmentsConfig.GetValue<string>("JwtIssuer");
                var audience = _environmentsConfig.GetValue<string>("JwtAudience");
                var key = Encoding.ASCII.GetBytes(_environmentsConfig.GetValue<string>(("JwtKey")));
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", check.Id.ToString()),
                        new Claim("Username", check.Username),

                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                AccessTokenGetResponse accessTokenGetResponse = new AccessTokenGetResponse();
                accessTokenGetResponse.Token = stringToken;
                modelResponse.Success();
                modelResponse.data = accessTokenGetResponse;
                await _memoryStore.SetAsync(check.Id.ToString(), stringToken, 60 * 60 * 24, "access-token");
            }

        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}