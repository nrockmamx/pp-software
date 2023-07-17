using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class PromotionAddLineCommand  :  IRequest<ModelResponse>
{   
    public PromotionAddLineRequest PromotionAddLineRequest { get; set; }
}