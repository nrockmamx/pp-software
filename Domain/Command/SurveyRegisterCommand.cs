using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class SurveyRegisterCommand  :  IRequest<ModelResponse>
{
    public SurveyRegisterRequest SurveyRegisterRequest { get; set; }
}