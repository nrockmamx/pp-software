using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MemberCardRegisterCommand  :  IRequest<ModelResponse>
{
    public MemberCarRegisterRequest MemberCarRegisterRequest = new MemberCarRegisterRequest();
}