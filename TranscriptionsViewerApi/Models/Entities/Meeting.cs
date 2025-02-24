using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TranscriptionsViewerApi.Models.Entities 
{
  public class Meeting 
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset MeetingDate { get; set;}
    public string Summary { get; set;}
  }

  public class MeetingConfiguration : IEntityTypeConfiguration<Meeting> 
  {
    public void Configure(EntityTypeBuilder<Meeting> builder) 
    {
      
    }
  }
}