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
    public string Summary { get; set;}
    
    [JsonIgnore]
    public NpgsqlTsVector SearchVector { get; set; }

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

      builder.Property(e => e.Summary)
        .HasMaxLength(2048);

       builder.HasGeneratedTsVectorColumn(
          e => e.SearchVector,
          "english",
          e => new { e.Title, e.Summary })
          .HasIndex(e => e.SearchVector)
          .HasMethod("GIN");
       
       builder.HasMany(e => e.TranscriptItems)
        .WithOne(e => e.Meeting)
        .HasForeignKey(e => e.MeetingId)
        .HasPrincipalKey(e => e.Id);
    }
  }
}