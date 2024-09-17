

using Ecommerce.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCommand command)
        {
            ContentResult response = await _mediator.Send(command);

            return new ContentResult
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }
       
    }
}
