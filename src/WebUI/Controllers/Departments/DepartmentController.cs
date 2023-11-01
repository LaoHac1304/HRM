using hrOT.Application.Departments;
using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments.Queries.GetTotalEmployees;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.Departments;

[Authorize(Policy = "manager")]
public class DepartmentController : ApiControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<DepartmentDTO>>> Get()
    {
        try 
        { 
            return await Mediator.Send(new GetListDepartmentQuery());
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetTotalEmployeeInDepartment")]
    public async Task<IActionResult> GetEmployeeInDepartment(Guid DepartmentId)
    {
        try
        {
            var result = await Mediator.Send(new GetListEmployeeInDepartmentQuery(DepartmentId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateDepartmentCommand command)
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
    public async Task<ActionResult> Update([FromForm] UpdateDepartmentCommand command)
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
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteDepartmentCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
           
            return BadRequest(ex.Message);
        }
    }
}