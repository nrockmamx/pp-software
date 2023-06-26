using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Command;
using Domain.Environments;
using Domain.Model;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.MongoDB.Collections;
using Domain.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;

namespace Domain.Handler;

public class MemberCardGenCommandHandler : IRequestHandler<MemberCardGenCommand, ModelResponse>
{
    private readonly IMongoRepository<MemberCardGenerate> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public MemberCardGenCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<MemberCardGenerate> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(MemberCardGenCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            int i = 0;
            List<string> cardId = new List<string>();
            while (true)
            {
                Random r = new Random();
                var key = $"{RandomString(3)}{r.Next(1000000,9999999)}";

                var check = _repository.GetCollection().AsQueryable().Where(x => x.CardId == key).FirstOrDefault();
                
                if(check!=null)
                    continue;

                MemberCardGenerate memberCardGenerate = new MemberCardGenerate();
                memberCardGenerate.CardId = key;
                await _repository.InsertOneAsync(memberCardGenerate);
                
                cardId.Add(key);

                i++;
                if (i >= 10)
                    break;
            }

            modelResponse.data = cardId;
            modelResponse.Success();
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
    
    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}