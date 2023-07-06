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

public class MonthlyJackpotUpdateCommandHandler : IRequestHandler<MonthlyJackpotUpdateCommand, ModelResponse>
{
    private readonly IMongoRepository<MonthlyJackpot> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public MonthlyJackpotUpdateCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<MonthlyJackpot> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(MonthlyJackpotUpdateCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            var check = _repository.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred).AsQueryable()
                .FirstOrDefault();

            if (check != null)
            {
                check = new MonthlyJackpot();
                check.Total = request.Total;
                check.Number1 = check.Total * 30 / 100;
                check.Number2 = check.Total * 20 / 100;
                check.Number3 = check.Total * 10 / 100;
                check.Number4 = check.Total * 5 / 100;
                check.Number5 = check.Total * 3 / 100;
                check.Number6 = check.Total * 2 / 100;
                check.Number7to20 = Convert.ToInt64(check.Total * 1.5 / 100);
                check.Number21to29 = Convert.ToInt64(check.Total * 1 / 100);
                await _repository.InsertOneAsync(check);
                
                var update = Builders<MonthlyJackpot>.Update;
                var updates = new List<UpdateDefinition<MonthlyJackpot>>();
                updates.Add(update.Set(x => x.Total, check.Total));
                updates.Add(update.Set(x => x.Number1, check.Number1));
                updates.Add(update.Set(x => x.Number2, check.Number2));
                updates.Add(update.Set(x => x.Number3, check.Number3));
                updates.Add(update.Set(x => x.Number4, check.Number4));
                updates.Add(update.Set(x => x.Number5, check.Number5));
                updates.Add(update.Set(x => x.Number6, check.Number6));
                updates.Add(update.Set(x => x.Number7to20, check.Number7to20));
                updates.Add(update.Set(x => x.Number21to29, check.Number21to29));
                await _repository.UpdateOneAsync(x => x.Id==check.Id, update.Combine(updates));
            }

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