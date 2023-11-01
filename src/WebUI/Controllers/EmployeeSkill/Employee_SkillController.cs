using hrOT.Application.Employees_Skill.Queries;
using hrOT.Application.EmployeeSkill.Commands;
using hrOT.Application.EmployeeSkill.Commands.Add;
using hrOT.Application.EmployeeSkill.Commands.Delete;
using hrOT.Application.EmployeeSkill.Commands.Update;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.EmployeeSkill;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Policy = "ManagerOrStaff")]
public class Employee_SkillController : ApiControllerBase
{
    [HttpGet("GetListSKill")]
    public async Task<IActionResult> GetListSKill(Guid EmployeeId)
    {
        try
        {
            if (EmployeeId == Guid.Empty)
            {
                return BadRequest("ID nhân viên rỗng hoặc chưa đúng định dạng.");
            }
            var result = await Mediator.Send(new Employee_GetListSkillQuery(EmployeeId));

            return Ok(result);
        }catch (Exception ex)
        { 
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("AddSkill")]
    public async Task<IActionResult> AddSkill([FromForm] Employee_CreateSkillCommand command)
    {
        try
        {

            var result = await Mediator.Send(command);

            return Ok(result);
        }catch (Exception ex)
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

    [HttpPut("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill([FromForm] Employee_UpdateSkillCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }catch (Exception ex)
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

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid EmployeeId, Guid SkillId)
    {
        try
        {
            if (EmployeeId == Guid.Empty)
            {
                return BadRequest("ID nhân viên rỗng hoặc chưa đúng định dạng.");
            }
            if (SkillId == Guid.Empty)
            {
                return BadRequest("ID kỹ năng rỗng hoặc chưa đúng định dạng.");
            }

            var result = await Mediator
                .Send(new Employee_DeleteSkillCommand(EmployeeId, SkillId));

            return Ok("Xóa thành công");
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}