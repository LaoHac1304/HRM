using hrOT.Application.OvertimeLogs.Commands.Create;
using hrOT.Application.OvertimeLogs.Commands.Delete;
using hrOT.Application.OvertimeLogs.Commands.Update;
using hrOT.Application.OvertimeLogs.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

//using hrOT.Application.OvertimeLogs.Commands.Create;
//using hrOT.Application.OvertimeLogs.Commands.Delete;
//using hrOT.Application.OvertimeLogs.Commands.Update;

namespace WebUI.Controllers.OvertimeLog;

[Route("api/[controller]")]
[ApiController]
public class OvertimeLogController : ApiControllerBase
{
    [HttpGet("GetAll")]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<List<OvertimeLogDto>>> GetList()
    {
        try
        {
            return await Mediator.Send(new Staff_GetListOvertimeLogQuery());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetListByEmployeeId")]
    //[Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<List<OvertimeLogDto>>> GetListByEmployeeId(Guid Id)
    {
        try
        {
            var result = await Mediator.Send(new Employee_GetListOvertimeLogByEmployeeIdQuery(Id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(Employee_CreateOvertimeLogCommand command)
    {
        try
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
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

    [HttpPut("UpdateStatus")]
    public async Task<ActionResult> UpdateStatus(Staff_UpdateOvertimeLogCommand command)
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

    [HttpPut("Update")]
    public async Task<ActionResult> Update(Employee_UpdateOvertimeLogCommand command)
    {
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");
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
    public async Task<ActionResult> Delete(DeleteOvertimeLogCommand command)
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