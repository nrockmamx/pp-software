using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MemberCardFindCommand  :  IRequest<ModelResponse>
{
    public MemberCardFindRequest MemberCardFindRequest { get; set; }
}