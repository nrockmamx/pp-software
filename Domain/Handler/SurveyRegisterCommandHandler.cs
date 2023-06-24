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

public class SurveyRegisterCommandHandler : IRequestHandler<SurveyRegisterCommand, ModelResponse>
{
    private readonly IMongoRepository<Survey> _repository;
    private readonly IEnvironmentsConfig _environmentsConfig;
    private readonly IMemoryStore _memoryStore;

    public SurveyRegisterCommandHandler( IEnvironmentsConfig environmentsConfig, IMemoryStore memoryStore, IMongoRepository<Survey> repository)
    {
        _environmentsConfig = environmentsConfig;
        _memoryStore = memoryStore;
        _repository = repository;
    }

    public async Task<ModelResponse> Handle(SurveyRegisterCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {
            Survey survey = new Survey();
            survey.Buget = request.SurveyRegisterRequest.Budget;
            survey.Like = request.SurveyRegisterRequest.Like;
            survey.Tel = request.SurveyRegisterRequest.Tel;
            survey.Province = request.SurveyRegisterRequest.Province;
            survey.NickName = request.SurveyRegisterRequest.NickName;
            survey.LineId = request.SurveyRegisterRequest.LineId;
            await _repository.InsertOneAsync(survey);
            modelResponse.Success();
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}