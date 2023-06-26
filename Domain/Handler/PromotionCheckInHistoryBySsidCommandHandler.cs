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

public class PromotionCheckInHistoryBySsidCommandHandler : IRequestHandler<PromotionCheckInHistoryBySsidCommand, ModelResponse>
{
    private readonly IMongoRepository<PromotionCheckIn> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public PromotionCheckInHistoryBySsidCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<PromotionCheckIn> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(PromotionCheckInHistoryBySsidCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {

            int skip = 0;

            if (request.Page > 1)
                skip = request.Page * 50;

            var check = _repository.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred).AsQueryable()
                .Where(x => x.IdenCard.Ssid == request.Ssid).OrderByDescending(x => x.CreatedAt).Skip(skip).Take(50)
                .ToList();

            modelResponse.data = check;
            modelResponse.Success();
            
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}