
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/external")]
public sealed class ExternalController(IHttpClientFactory http) : ControllerBase
{
    [HttpGet("geo")]
    public async Task<ActionResult> Geo(CancellationToken ct)
    {
        var client = http.CreateClient("geo");
        var res = await client.GetAsync("json/8.8.8.8", ct);
        var body = await res.Content.ReadAsStringAsync(ct);
        return Content(body, "application/json");
    }
}
