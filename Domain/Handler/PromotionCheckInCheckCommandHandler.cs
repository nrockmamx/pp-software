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

public class PromotionCheckInCheckCommandHandler : IRequestHandler<PromotionCheckInCheckCommand, ModelResponse>
{
    private readonly IMongoRepository<PromotionCheckIn> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public PromotionCheckInCheckCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<PromotionCheckIn> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(PromotionCheckInCheckCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            var check = _repository.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred).AsQueryable()
                .Where(x => x.PersonalNo == request.Ssid && x.Dt == DateTime.Now.Date.ToString("dd-MM-yyyy")).FirstOrDefault();

            if (check == null)
            {
                modelResponse.Success();
            }

        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}