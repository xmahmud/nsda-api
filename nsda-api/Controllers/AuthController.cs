using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nsda_api.Domain.Commands;

namespace nsda_api.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthCommand command)
        {
            var model = await _mediator.Send(command);

            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return Unauthorized(
                    new
                    {
                        Status = 401,
                        Message = "Email/Password did not match"
                    }
                    );
            }
        }
    }
}
