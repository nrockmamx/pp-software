using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class PromotionCheckInCheckCommand  :  IRequest<ModelResponse>
{   
    public string Ssid { get; set; }
}