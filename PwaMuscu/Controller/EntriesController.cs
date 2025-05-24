using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PwaMuscu.Models;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]

public class EntriesController : ControllerBase
{
    private readonly SupabaseService _service;

    public EntriesController(SupabaseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> Get() => Ok(await _service.GetEntriesAsync());

    [HttpPost]
    public async Task<ActionResult> Post(PoidsEntry entry)
    {
        await _service.AddEntryAsync(entry);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteEntryAsync(id);
        return Ok();
    }
}
