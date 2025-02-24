using Microsoft.AspNetCore.Mvc;

namespace TranscriptionsViewerApi.Controllers 
{
  [ApiController]
  [Route("[controller]")]
  public class TranscriptionsController : Controller 
  {

    public TranscriptionsController()
    {

    }

    [HttpGet("IsAlive")]
    public IActionResult IsAlive() {
      return Ok("Is Alive");
    }
  }
}