namespace TranscriptionsViewerApi.Models.DTOs 
{
  public class RankedTranscriptItem 
  {
    public int Id { get; set; }
    public int MeetingId { get; set; }
    public int TimestampStart { get; set; }
    public int TimestampEnd { get; set; }
    public string Text { get; set; }
    public float Rank { get; set; }
  }
}