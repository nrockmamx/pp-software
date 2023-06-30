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

public class MemberCardRegisterCommandHandler : IRequestHandler<MemberCardRegisterCommand, ModelResponse>
{
    private readonly IMongoRepository<MemberCardGenerate> _repositoryMemberGen;
    private readonly IMongoRepository<MemberCard> _repositoryMember;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public MemberCardRegisterCommandHandler(IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore,
        IMongoRepository<MemberCardGenerate> repositoryMemberGen, IMongoRepository<MemberCard> repositoryMember)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repositoryMemberGen = repositoryMemberGen;
        _repositoryMember = repositoryMember;
    }

    public async Task<ModelResponse> Handle(MemberCardRegisterCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {

            
            if (string.IsNullOrEmpty(request.MemberCardRegisterRequest.NickName))
            {
                modelResponse.data = "NICKNAME_ERROR";
                return modelResponse;
            }
            
            if (string.IsNullOrEmpty(request.MemberCardRegisterRequest.Tel) && request.MemberCardRegisterRequest.Tel.Length<10)
            {
                modelResponse.data = "TEL_ERROR";
                return modelResponse;
            }

            var checkTel = _repositoryMember.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred)
                .AsQueryable().Where(x => x.IdenCard.Tel == request.MemberCardRegisterRequest.Tel)
                .FirstOrDefault();

            if (checkTel != null)
            {
                modelResponse.data = "TEL_USED";
                return modelResponse;
            }
            
            MemberCard memberCard = new MemberCard();
            memberCard.CardId = request.MemberCardRegisterRequest.CardId;
            memberCard.NickName = request.MemberCardRegisterRequest.NickName;
            memberCard.Tel = request.MemberCardRegisterRequest.Tel;

            if (request.MemberCardRegisterRequest.CardIden != null)
            {
                memberCard.IdenCard = new IdenCard();
                memberCard.IdenCard.Ssid = request.MemberCardRegisterRequest.CardIden.Ssid;
                memberCard.IdenCard.Tel = request.MemberCardRegisterRequest.Tel;
                memberCard.RegisterDate = DateTime.Now;
                memberCard.IdenCard.Address = request.MemberCardRegisterRequest.CardIden.Address;
                memberCard.IdenCard.NameEng = request.MemberCardRegisterRequest.CardIden.NameEng;
                memberCard.IdenCard.NameTh = request.MemberCardRegisterRequest.CardIden.NameTh;
                memberCard.IdenCard.PassportImage = request.MemberCardRegisterRequest.CardIden.PassportImage;
                memberCard.IdenCard.PersonalImage = request.MemberCardRegisterRequest.CardIden.PersonalImage;
                memberCard.IdenCard.DateOfBirth = request.MemberCardRegisterRequest.CardIden.DateOfBirth;
            }

            var update = Builders<MemberCardGenerate>.Update;
            var updates = new List<UpdateDefinition<MemberCardGenerate>>();
            updates.Add(update.Set(x => x.Used, true));
            await _repositoryMemberGen.UpdateOneAsync(x => x.CardId == request.MemberCardRegisterRequest.CardId, update.Combine(updates));
            await _repositoryMember.InsertOneAsync(memberCard);
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