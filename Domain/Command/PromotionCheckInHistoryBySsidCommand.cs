using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class PromotionCheckInHistoryBySsidCommand  :  IRequest<ModelResponse>
{   
    public string Ssid { get; set; }
    public int Page { get; set; }
}