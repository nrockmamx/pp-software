using Domain.Command;
using Domain.Model;
using MediatR;

namespace Domain.Handler;

public class PromotionAddLineCommandHandler : IRequestHandler<PromotionAddLineCommand, ModelResponse>
{
    public async Task<ModelResponse> Handle(PromotionAddLineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}