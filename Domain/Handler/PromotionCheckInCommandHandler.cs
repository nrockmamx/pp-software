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

public class PromotionCheckInCommandHandler : IRequestHandler<PromotionCheckInCommand, ModelResponse>
{
    private readonly IMongoRepository<IdenCard> _repositoryIdenCard;
    private readonly IMongoRepository<PromotionCheckIn> _repositoryPromotionCheckIn;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public PromotionCheckInCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<IdenCard> repositoryIdenCard, IMongoRepository<PromotionCheckIn> repositoryPromotionCheckIn)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repositoryIdenCard = repositoryIdenCard;
        _repositoryPromotionCheckIn = repositoryPromotionCheckIn;
    }

    public async Task<ModelResponse> Handle(PromotionCheckInCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {


            var checkPro = _repositoryPromotionCheckIn.GetCollection()
                .WithReadPreference(ReadPreference.SecondaryPreferred).AsQueryable()
                .Where(x => x.PersonalNo== request.PromotionCheckInRequest.PassportInfo.PersonalNo && x.Dt==DateTime.Now.Date.ToString("dd-MM-yyyy")).FirstOrDefault();

            if (checkPro != null)
            {
                modelResponse.data = "ALREADY";
                return modelResponse;
            }
                
            PromotionCheckIn promotionCheckIn = new PromotionCheckIn();
            promotionCheckIn.PassportInfo = new PassportInfo();
            promotionCheckIn.PassportInfo = request.PromotionCheckInRequest.PassportInfo;
            promotionCheckIn.PersonalNo = request.PromotionCheckInRequest.PassportInfo.PersonalNo;
            promotionCheckIn.CheckInTime = DateTime.Now;
            promotionCheckIn.PhoneNumber = request.PromotionCheckInRequest.PhoneNumber;
            promotionCheckIn.Amount = request.PromotionCheckInRequest.Amount;
            promotionCheckIn.PassportImage = request.PromotionCheckInRequest.PassportImage;
            promotionCheckIn.CameraImage = request.PromotionCheckInRequest.CameraImage;
            promotionCheckIn.Dt = DateTime.Now.Date.ToString("dd-MM-yyyy");
            await _repositoryPromotionCheckIn.InsertOneAsync(promotionCheckIn);
            modelResponse.Success();
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}