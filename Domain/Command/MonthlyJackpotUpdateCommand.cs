using Domain.Model;
using Domain.Model.Request;
using MediatR;

namespace Domain.Command;

public class MonthlyJackpotUpdateCommand  :  IRequest<ModelResponse>
{   
    public long Total { get; set; }
}