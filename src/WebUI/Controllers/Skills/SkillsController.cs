using hrOT.Application.Common.Exceptions;
using hrOT.Application.Skills.Commands;
using hrOT.Application.Skills.Commands.Add;
using hrOT.Application.Skills.Commands.Delete;
using hrOT.Application.Skills.Commands.Update;
using hrOT.Application.Skills.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.Skills;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "manager")]
public class SkillsController : ApiControllerBase
{
    [HttpGet("GetSkillsList")]
    public async Task<ActionResult<List<SkillDTO>>> GetSkillsList()
    {
        /*try
        {
            var result = await Mediator.Send(new GetSkillListQuery());
            return result != null
            ? Ok(result) : BadRequest("Danh sách kĩ năng trống.");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            var errorsDictionary = new Dictionary<string, string[]>(ex.Errors);
            return BadRequest(errorsDictionary);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }*/
        try
        {
            return await Mediator.Send(new GetSkillListQuery());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddSkill")]
    public async Task<ActionResult<Guid>> AddSkill([FromForm] CreateSkillCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
            /*return result == true
                ? Ok("Thêm thành công")
                : BadRequest("Lỗi xảy ra, không thể thêm kĩ năng.");*/
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

    [HttpPut("UpdateSkill")]
    public async Task<ActionResult> UpdateSkill([FromForm] UpdateSkillCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
            /*return result.Contains("thành công")
                ? Ok(result)
                : BadRequest(result);*/
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
        /*catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            var errorsDictionary = new Dictionary<string, string[]>(ex.Errors);
            return BadRequest(errorsDictionary);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }*/
    }

    [HttpDelete("DeleteSkill")]
    public async Task<ActionResult> DeleteSkill(Guid SkillId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteSkillCommand(SkillId));
            return Ok("Xóa thành công");
            /*return result.Contains("thành công")
                ? Ok(result)
                : BadRequest(result);*/
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
        /*catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            var errorsDictionary = new Dictionary<string, string[]>(ex.Errors);
            return BadRequest(errorsDictionary);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }*/
    }
}