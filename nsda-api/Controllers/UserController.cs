using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nsda_api.Domain.Commands;
using nsda_api.Domain.Queries;
using nsda_api.Domain.ViewModels;

namespace nsda_api.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserCommand command)
        {
            var model = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UpdateUserCommand command)
        {
            var model = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
        {
            var model = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpPut("upgrade")]
        [AllowAnonymous]
        public async Task<IActionResult> UpgradeUserCommand([FromBody] UpgradeUserCommand command)
        {
            var model = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpPut("active")]
        [AllowAnonymous]
        public async Task<IActionResult> UserActiveDeactiveComand([FromBody] UserActiveDeactiveComand command)
        {
            var model = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInformationQuery(Guid id)
        {
            var q = new GetUserInformationQuery
            {
                UserId = id
            };
            var model = await _mediator.Send(q);

            return Ok(model);
        }

        [HttpGet("users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersQuery([FromQuery] int? membershipIndex)
        {
            var q = new GetUsersQuery
            {
                MembershipIndex = membershipIndex
            };
            var model = await _mediator.Send(q);

            return Ok(model);
        }
    }
}
