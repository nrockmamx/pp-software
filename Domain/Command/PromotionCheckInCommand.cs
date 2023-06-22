using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class PromotionCheckInCommand  :  IRequest<ModelResponse>
{
    public PromotionCheckInRequest PromotionCheckInRequest { get; set; }
}