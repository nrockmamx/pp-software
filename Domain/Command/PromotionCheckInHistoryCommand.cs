using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class PromotionCheckInHistoryCommand  :  IRequest<ModelResponse>
{   
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}