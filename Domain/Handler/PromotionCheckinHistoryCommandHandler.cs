using Domain.Command;
using Domain.Model;
using MediatR;
using Serilog;

namespace Domain.Handler;

public class PromotionCheckinHistoryCommandHandler : IRequestHandler<PromotionCheckInHistoryCommand, ModelResponse>
{
    public async Task<ModelResponse> Handle(PromotionCheckInHistoryCommand request, CancellationToken cancellationToken)
    {
        ModelResponse modelResponse = new ModelResponse();
        try
        {

        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

        return modelResponse;
    }
}