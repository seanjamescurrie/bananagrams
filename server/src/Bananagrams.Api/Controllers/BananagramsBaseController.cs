using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

public class BananagramsBaseController : ControllerBase
{
    protected ActionResult OkOrNoContent(object value)
    {
        if (HasNoValueOrItems(value)) return NoContent();
            
        return Ok(value);
    }
    
        
    protected ActionResult OkOrNoNotFound(object value)
    {
        if (HasNoValueOrItems(value)) return NotFound();
            
        return Ok(value);
    }

    private static bool HasNoValueOrItems(object value) => value == null || value is IList {Count: < 1};
}
