using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identidade.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                sucess = true,
                data = result
            });
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddError(error.ErrorMessage);
        }

        return CustomResponse();
    }
    protected bool ValidOperation()
    {
        return !Errors.Any();
    }
    
    protected void AddError(string error)
    {
        Errors.Add(error);
    }
    
    protected void ClearErrors()
    {
        Errors.Clear();
    }
}