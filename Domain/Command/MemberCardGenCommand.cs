using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MemberCardGenCommand  :  IRequest<ModelResponse>
{
}