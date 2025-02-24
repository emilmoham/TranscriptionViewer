using Microsoft.EntityFrameworkCore;
using TranscriptionsViewerApi.Models.Entities;

namespace TranscriptionsViewerApi 
{
  public class ApplicationContext : DbContext
  {
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<TranscriptItem> TranscriptItems { get; set; }

    public ApplicationContext() {}
    public ApplicationContext(DbContextOptions<ApplicationContext> options ) : base( options ) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

    }
  }
}