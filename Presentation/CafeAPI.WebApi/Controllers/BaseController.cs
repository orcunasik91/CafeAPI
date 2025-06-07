using CafeAPI.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult CreateResponse<T>(ResponseDto<T> response) where T : class
    {
        if (response.Success)
            return Ok(response);
        return response.ErrorCode switch
        {
            ErrorCodes.NotFound => NotFound(response),
            ErrorCodes.BadRequest => BadRequest(response),
            ErrorCodes.ValidationError => BadRequest(response),
            ErrorCodes.Unauthorized => Unauthorized(response),
            ErrorCodes.DuplicateError => Conflict(response),
            ErrorCodes.Forbidden => StatusCode(StatusCodes.Status403Forbidden,response),
            ErrorCodes.Exception => StatusCode(StatusCodes.Status500InternalServerError,response),
            _=>BadRequest(response)
        };
    }
}