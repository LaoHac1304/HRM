
using FluentValidation;
using hrOT.Application.Allowances.Command.Create;
using hrOT.Application.Allowances.Command.Delete;
using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.Allowance;

public class AllowanceController : ApiControllerBase
{
    [HttpGet("GetAll")]
    //[Authorize(Policy = "manager")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var result = await Mediator.Send(new GetListAllowanceQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetListByEmployeeId")]
    //[Authorize(Policy = "manager")]
    public async Task<IActionResult> GetListByEmployeeId(Guid EmployeeId)
    {
        if (EmployeeId == Guid.Empty) { return BadRequest("EmployeeId trống hoặc không đúng định dạng"); }
        try
        {
            var result = await Mediator.Send(new GetListAllowanceByEmployeeIdQuery(EmployeeId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Create")]    
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreateAllowanceCommand command)
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

    [HttpPost("CreateMulti")]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> CreateMulti(CreateMultiAllowanceCommand command)
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
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult> Update(UpdateAllowanceCommand command)
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
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete(DeleteAllowanceCommand command)
    {
        
        try
        {
            await Mediator.Send(command);
            return Ok("Xóa thành công.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}