using Microsoft.AspNetCore.Mvc;
using TranscriptionsViewerApi.Models.Entities;
using TranscriptionsViewerApi.Services;

namespace TranscriptionsViewerApi.Controllers 
{
  [ApiController]
  [Route("[controller]")]
  public class TranscriptionsController : Controller 
  {
    private readonly TranscriptionsService _trancriptionsService;

    public TranscriptionsController(TranscriptionsService transcriptionsService)
    {
      _trancriptionsService = transcriptionsService;
    }

    [HttpGet("IsAlive")]
    public IActionResult IsAlive() {
      return Ok("Is Alive");
    }

    [HttpGet("Meetings")]
    public async Task<IActionResult> GetMeetings() 
    {
      IEnumerable<Meeting> meetings = await _trancriptionsService.GetMeetings();
      return Ok(meetings);
    }

    [HttpGet("Meeting/{id}")]
    public async Task<IActionResult> GetMeeting(int id) {
      Meeting? meeting = await _trancriptionsService.GetMeetingById(id);
      if (meeting == null)
        return NotFound();
      return Ok(meeting);
    }
  }
}