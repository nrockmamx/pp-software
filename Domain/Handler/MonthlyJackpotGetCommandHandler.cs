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

public class MonthlyJackpotGetCommandHandler : IRequestHandler<MonthlyJackpotGetCommand, ModelResponse>
{
    private readonly IMongoRepository<MonthlyJackpot> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public MonthlyJackpotGetCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<MonthlyJackpot> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(MonthlyJackpotGetCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            var check = _repository.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred).AsQueryable()
                .FirstOrDefault();

            if (check == null)
            {
                check = new MonthlyJackpot();
                check.Total = 100000;
                check.Number1 = check.Total * 30 / 100;
                check.Number2 = check.Total * 20 / 100;
                check.Number3 = check.Total * 10 / 100;
                check.Number4 = check.Total * 5 / 100;
                check.Number5 = check.Total * 3 / 100;
                check.Number6 = check.Total * 2 / 100;
                check.Number7to20 = Convert.ToInt64(check.Total * 1.5 / 100);
                check.Number21to29 = Convert.ToInt64(check.Total * 1 / 100);
                await _repository.InsertOneAsync(check);
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