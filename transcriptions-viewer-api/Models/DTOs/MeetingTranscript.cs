namespace TranscriptionsViewerApi.Models.DTOs 
{
  public class MeetingTranscript 
  {
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? AudioUrl { get; set; }
    public string? Summary { get; set; }
    public string? VttData { get; set;}
  }
}