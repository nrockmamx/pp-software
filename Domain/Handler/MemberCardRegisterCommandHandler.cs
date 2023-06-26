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
            if (request.MemberCardRegisterRequest.CardId.Length < 10)
            {
                modelResponse.data = "CARDID_ERROR";
                return modelResponse;
            }

            var check = _repositoryMemberGen.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred)
                .AsQueryable().Where(x => x.CardId == request.MemberCardRegisterRequest.CardId)
                .FirstOrDefault();

            if (check == null)
            {
                modelResponse.data = "CANT_FIND_CARDID";
                return modelResponse;
            }

            if (check.Used)
            {
                modelResponse.data = "CARDID_USED";
                return modelResponse;
            }

            var checkMember = _repositoryMember.GetCollection().WithReadPreference(ReadPreference.SecondaryPreferred)
                .AsQueryable().Where(x => x.CardId == request.MemberCardRegisterRequest.CardId)
                .FirstOrDefault();

            if (checkMember != null)
            {
                modelResponse.data = "CARDID_USED";
                return modelResponse;
            }
            
            if (string.IsNullOrEmpty(request.MemberCardRegisterRequest.NickName))
            {
                modelResponse.data = "NICKNAME_ERROR";
                return modelResponse;
            }

            MemberCard memberCard = new MemberCard();
            memberCard.CardId = request.MemberCardRegisterRequest.CardId;
            memberCard.NickName = request.MemberCardRegisterRequest.NickName;

            if (request.MemberCardRegisterRequest.CardIden != null)
            {
                memberCard.IdenCard = new IdenCard();
                memberCard.IdenCard.Ssid = request.MemberCardRegisterRequest.CardIden.Ssid;
                memberCard.IdenCard.Tel = request.MemberCardRegisterRequest.Tel;
                memberCard.RegisterDate = DateTime.Now;
                memberCard.IdenCard.AddressEng = request.MemberCardRegisterRequest.CardIden.AddressEng;
                memberCard.IdenCard.AddressTh = request.MemberCardRegisterRequest.CardIden.AddressTh;
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