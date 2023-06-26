using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MemberGenCommand  :  IRequest<ModelResponse>
{
}