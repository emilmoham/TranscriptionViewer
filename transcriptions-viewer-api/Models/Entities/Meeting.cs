using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;

namespace TranscriptionsViewerApi.Models.Entities 
{
  public class Meeting 
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset MeetingDate { get; set;}
    public string RecordingKey { get; set; }
    public string CaptionsKey { get; set; }
    public string Summary { get; set;}
    
    [JsonIgnore]
    public NpgsqlTsVector TitleSearchVector { get; set; }
    [JsonIgnore]
    public NpgsqlTsVector SummarySearchVector { get; set; }

    public ICollection<TranscriptItem> TranscriptItems { get; set; }
  }

  public class MeetingConfiguration : IEntityTypeConfiguration<Meeting> 
  {
    public void Configure(EntityTypeBuilder<Meeting> builder) 
    {
      builder.Property(e => e.Id)
        .UseIdentityColumn();

      builder.Property(e => e.Title)
        .HasMaxLength(254);

       builder.HasGeneratedTsVectorColumn(
          e => e.TitleSearchVector,
          "english",
          e => e.Title)
          .HasIndex(e => e.TitleSearchVector)
          .HasMethod("GIN");

        builder.HasGeneratedTsVectorColumn(
          e => e.SummarySearchVector,
          "english",
          e => e.Summary)
          .HasIndex(e => e.SummarySearchVector)
          .HasMethod("GIN");
       
       builder.HasMany(e => e.TranscriptItems)
        .WithOne(e => e.Meeting)
        .HasForeignKey(e => e.MeetingId)
        .HasPrincipalKey(e => e.Id);
    }
  }
}