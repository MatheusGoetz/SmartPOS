
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartPOS.Application.Features.Categories.CreateCategory;

namespace SmartPOS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public sealed class CategoriesController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoriesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryResponse),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCategoryResponse>> Create([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}