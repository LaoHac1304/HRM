using hrOT.Application.Common.Exceptions;
using hrOT.Application.LeaveTypes.Commands.Create;
using hrOT.Application.LeaveTypes.Commands.Delete;
using hrOT.Application.LeaveTypes.Commands.Update;
using hrOT.Application.LeaveTypes.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.LeaveType;

public class LeaveTypeController : ApiControllerBase
{

    [HttpGet("GetAll")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var result = await Mediator.Send(new Manager_GetlistLeaveTypeQuery());
            return result.Count > 0
                ? Ok(result) :
                BadRequest("Danh sách trống");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("Create")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(Manager_CreateLeaveTypeCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }

    [HttpPut("Update")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus(Manager_UpdateLeaveTypeCommand command)
    {
       
        try
        {
            Guid rs = await Mediator.Send(command);
            return Ok(rs);
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
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await Mediator.Send(new Manager_DeleteLeaveTypeCommand { LeaveTypeId = new Guid(id) });
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
