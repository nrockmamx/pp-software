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

public class MemberCardFindCommandHandler : IRequestHandler<MemberCardFindCommand, ModelResponse>
{
    private readonly IMongoRepository<MemberCard> _repositoryMemberCard;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public MemberCardFindCommandHandler(IMongoRepository<MemberCard> repositoryMemberCard, IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore)
    {
        _repositoryMemberCard = repositoryMemberCard;
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
    }

    public async Task<ModelResponse> Handle(MemberCardFindCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            if (string.IsNullOrEmpty(request.MemberCardFindRequest.CardId) &&
                string.IsNullOrEmpty(request.MemberCardFindRequest.Name))
            {
                modelResponse.data = "ERROR_INPUT";
                return modelResponse;
            }

            var check = _repositoryMemberCard.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred)
                .AsQueryable().Where(x => x.CardId == request.MemberCardFindRequest.CardId)
                .FirstOrDefault();

            if (check != null)
            {
                modelResponse.data = check;
                modelResponse.Success();
                return modelResponse;
            }
            
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}