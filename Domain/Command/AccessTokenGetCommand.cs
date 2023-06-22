using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class AccessTokenGetCommand  :  IRequest<ModelResponse>
{   
    public AccessTokenGetRequest AccessTokenGetRequest { get; set; }
}