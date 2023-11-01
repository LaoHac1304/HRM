using hrOT.Application.Common.Exceptions;
using hrOT.Application.TaxInComes.Commands.CreateTaxInCome;
using hrOT.Application.TaxInComes.Commands.DeleteTaxInCome;
using hrOT.Application.TaxInComes.Commands.UpdateTaxInCome;
using hrOT.Application.TaxInComes.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.TaxInComes;
[Authorize(Policy = "Manager")]

public class TaxInComeController : ApiControllerBase
{
    [HttpGet("Get")]
    public async Task<ActionResult<List<TaxInCome>>> Get()
    {
        try
        {
            return await Mediator.Send(new GetListTaxInComeQuery());
        } catch(Exception ex)
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
    public async Task<ActionResult<Guid>> Create([FromForm] CreateTaxInComeCommand command)
    {
        try
        {
            if (ModelState.IsValid && command != null)
            {
                await Mediator.Send(command);
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
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
    public async Task<ActionResult> Update([FromForm] UpdateTaxInComeCommand command)
    {
        
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        //catch (NotFoundException ex)
        //{
        //    return NotFound(ex.Message);
        //}
        //catch (InvalidOperationException ex)
        //{
        //    return BadRequest(ex.Message);
        //}
        //catch (ValidationException ex)
        //{
        //    var errorsDictionary = new Dictionary<string, string[]>(ex.Errors);
        //    return BadRequest(errorsDictionary);
        //}
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

    [HttpDelete("Delete{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteTaxInComeCommand(id));
            return Ok("Xóa thành công");
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}