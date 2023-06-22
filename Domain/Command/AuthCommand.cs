using Domain.Model;
using MediatR;

namespace Domain.Command;

public class AuthCommand  :  IRequest<ModelResponse>
{
    public string Token { get; set; }
}