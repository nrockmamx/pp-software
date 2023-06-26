using Api.ExtensionService;
using Domain.Command;
using Domain.Model;
using Domain.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;


namespace Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status500InternalServerError)]
    [Route("v1/promotion/")]
    public class PromotionController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IMediator _mediator;

        public PromotionController( IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("borderpass/checkin")]
        [AuthTokenRequired]
        public async Task<ActionResult<ModelResponse>> SumPromotion(PromotionCheckInRequest promotionCheckInRequest,CancellationToken cancellationToken = default)
        {
            try
            {
                var resp = await _mediator.Send(new PromotionCheckInCommand()
                {
                    PromotionCheckInRequest = promotionCheckInRequest
                });
 
                return resp;
                return null;
            }
            catch (Exception e)
            {
                _logger.Error("{Message}",e.ToString());
                return BadRequest();
            }
        }
        
        [HttpGet("borderpass/check/{ssid}")]
        [AuthTokenRequired]
        public async Task<ActionResult<ModelResponse>> PromotionCheckInCheck(string ssid,CancellationToken cancellationToken = default)
        {
            try
            {
                var resp = await _mediator.Send(new PromotionCheckInCheckCommand()
                {
                    Ssid = ssid
                });
 
                return resp;
                return null;
            }
            catch (Exception e)
            {
                _logger.Error("{Message}",e.ToString());
                return BadRequest();
            }
        }
        
        [HttpGet("borderpass/history/{ssid}/{page}")]
        [AuthTokenRequired]
        public async Task<ActionResult<ModelResponse>> PromotionCheckInHistory(string ssid,int page = 1,CancellationToken cancellationToken = default)
        {
            try
            {
                var resp = await _mediator.Send(new PromotionCheckInHistoryBySsidCommand()
                {
                    Ssid = ssid,
                    Page = page
                });
 
                return resp;
                return null;
            }
            catch (Exception e)
            {
                _logger.Error("{Message}",e.ToString());
                return BadRequest();
            }
        }
    }
}