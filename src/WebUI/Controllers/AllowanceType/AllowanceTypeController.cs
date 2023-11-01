using hrOT.Application.AllowanceTypes.Command.Create;
using hrOT.Application.AllowanceTypes.Command.Delete;
using hrOT.Application.AllowanceTypes.Command.Update;
using hrOT.Application.AllowanceTypes.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.AllowanceType;
public class AllowanceTypeController : ApiControllerBase
{
    [HttpGet("GetAll")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var result = await Mediator.Send(new GetListAllowanceTypesQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Create")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreateAllowanceTypeCommand command)
    {
        try
        {
            var request = await Mediator.Send(command);
            return Ok(request);
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

    [HttpPut("Update")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Update(UpdateAllowanceTypeCommand command)
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

    [HttpDelete("Delete")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete(DeleteAllowanceTypeCommand command)
    {
        try
        {
            await Mediator.Send(command);
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
