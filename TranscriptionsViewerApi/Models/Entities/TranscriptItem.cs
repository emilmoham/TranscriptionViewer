using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TranscriptionsViewerApi.Models.Entities
{
  public class TranscriptItem 
  {
    public int Id { get; set; }
    public int MeetingId { get; set; }
    public int TimestampStart { get; set; }
    public int TimestampEnd { get; set; }
    public string Text { get; set;}
  }

  public class TranscriptItemConfiguration : IEntityTypeConfiguration<TranscriptItem> 
  {
    public void Configure(EntityTypeBuilder<TranscriptItem> builder) 
    {
      
    }
  }
}