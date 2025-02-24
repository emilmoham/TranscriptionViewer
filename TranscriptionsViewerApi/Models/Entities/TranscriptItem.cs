using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;

namespace TranscriptionsViewerApi.Models.Entities
{
  public class TranscriptItem 
  {
    public int Id { get; set; }
    public int MeetingId { get; set; }
    public int TimestampStart { get; set; }
    public int TimestampEnd { get; set; }
    public string Text { get; set; }

    public NpgsqlTsVector SearchVector { get; set; }

    public Meeting Meeting { get; set; }
  }

  public class TranscriptItemConfiguration : IEntityTypeConfiguration<TranscriptItem> 
  {
    public void Configure(EntityTypeBuilder<TranscriptItem> builder) 
    {
      builder.Property(e => e.Id)
        .UseIdentityColumn();

      builder.HasGeneratedTsVectorColumn(
          e => e.SearchVector,
          "english",
          e => e.Text)
          .HasIndex(e => e.SearchVector)
          .HasMethod("GIN");
    }
  }
}