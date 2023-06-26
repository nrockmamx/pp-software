using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MemberCardPrintedCommand  :  IRequest<ModelResponse>
{
    public string CardId { get; set; }
}