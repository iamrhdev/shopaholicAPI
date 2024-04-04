using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopaholic.Application.CommandsQueries.Accounts.Commands;
using Shopaholic.Application.DTOs.Accounts;

namespace Shopaholic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _mediator.Send(new UserRegisterCommand(userRegisterDto));
            return Created();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserSignInDto userSignInDto)
        {
            await _mediator.Send(new UserLoginCommand(userSignInDto));
            return Ok();
        }
    }
}
