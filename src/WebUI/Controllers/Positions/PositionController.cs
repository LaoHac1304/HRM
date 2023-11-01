using hrOT.Application.Positions;
using hrOT.Application.Positions.Commands.CreatePosition;
using hrOT.Application.Positions.Commands.DeletePosition;
using hrOT.Application.Positions.Commands.UpdatePosition;
using hrOT.Application.Positions.Queries.GetPosition;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.Positions;

[Authorize(Policy = "manager")]
public class PositionController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PositionDTO>>> Get()
    {
        try
        {
            return await Mediator.Send(new GetListPositionQuery());
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreatePositionCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromForm] UpdatePositionCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid PositionId)
    {
        try
        {
            await Mediator.Send(new DeletePositionCommand(PositionId));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }
}