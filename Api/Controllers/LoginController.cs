using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;


namespace Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ModelResponse),StatusCodes.Status500InternalServerError)]
    [Route("v1/login/")]
    public class LoginController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IMediator _mediator;

        public LoginController( IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("accesstoken/get")]
        public async Task<ActionResult<ModelResponse>> SumPromotion(DateTime dt, CancellationToken cancellationToken = default)
        {
            try
            {
                /*var resp = await _mediator.Send(new SumPromotionCommand()
                {
                    Dt = dt
                });
 
                return resp;*/
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