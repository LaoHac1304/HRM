using hrOT.Application.Common.Exceptions;
using hrOT.Application.LeaveLogs.Commands.Create;
using hrOT.Application.LeaveLogs.Commands.Delete;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Application.LeaveLogs.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.LeaveLog;

public class LeaveLogController : ApiControllerBase
{
    [HttpGet("GetAll")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var result = await Mediator.Send(new Staff_GetListLeaveLogQuery());
            return result.Count > 0
                ? Ok(result) :
                BadRequest("Danh sách trống");
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

    [HttpGet("GetByDate")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> GetListByDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            var result = await Mediator.Send(new Staff_GetListLeaveLogByDateQuery(startDate, endDate));
            return result.Count > 0
                ? Ok(result)
                : BadRequest("Danh sách trống");
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

    [HttpPost("Create")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<string>> Create([FromForm] Employee_CreateLeaveLogCommand command)
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

    [HttpPut("Staff/Update")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus([FromForm] Staff_UpdateLeaveLogCommand command)
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
            else if (ex is NotFoundException)
            {
                return NotFound(ex.Message);
            }
            else return BadRequest(ex.Message);
        }
    }

    [HttpPut("Employee/Update")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> Update([FromForm] Employee_UpdateLeaveLogCommand command)
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
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteLeaveLogCommand(id));
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
}