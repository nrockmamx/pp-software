using Domain.Command;
using Domain.Model;
using Domain.Model.Request;
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

        [HttpPost("accesstoken/get")]
        public async Task<ActionResult<ModelResponse>> AccessTokenGet(AccessTokenGetRequest accessTokenGetRequest)
        {
            try
            {
                var resp = await _mediator.Send(new AccessTokenGetCommand()
                {
                    AccessTokenGetRequest = accessTokenGetRequest
                });
 
                return resp;
            }
            catch (Exception e)
            {
                _logger.Error("{Message}",e.ToString());
                return BadRequest();
            }
        }
    }
}