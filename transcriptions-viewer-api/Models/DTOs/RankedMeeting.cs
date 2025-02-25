using TranscriptionsViewerApi.Models.Entities;

namespace TranscriptionsViewerApi.Models.DTOs 
{
  public class RankedMeeting {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset MeetingDate { get; set;}
    public string RecordingKey { get; set; }
    public string CaptionsKey { get; set; }
    public string Summary { get; set;}
    public float Rank { get; set; }

    public ICollection<TranscriptItem> TranscriptItems { get; set; }
  }
}