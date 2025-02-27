using Bogus;
using TranscriptionsViewerApi.Models.Entities;
using TranscriptionsViewerApi.Repositories;

namespace TranscriptionsViewerApi.Seeders 
{
  public class MeetingSeeder
  {
    // private readonly ApplicationContext _applicationContext;
    private readonly ITranscriptionsRepository _transcriptionsRepository;

    public MeetingSeeder(
      ITranscriptionsRepository transcriptionsRepository
    ) {
      _transcriptionsRepository = transcriptionsRepository;
    }

    public async Task SeedAsync() {
      Faker<Meeting> meetingFaker = new Faker<Meeting>()
        .RuleFor(m => m.Title, f => f.WaffleTitle())
        .RuleFor(m => m.MeetingDate, f => f.Date.PastOffset().UtcDateTime)
        .RuleFor(m => m.RecordingKey, f => f.System.FilePath())
        .RuleFor(m => m.CaptionsKey, f => f.System.FilePath())
        .RuleFor(m => m.Summary, f => f.WaffleMarkdown(1, false));

      List<Meeting> testData = meetingFaker.Generate(100);
      
      _transcriptionsRepository.AddMeetings(testData);
      await _transcriptionsRepository.SaveChangesAsync();
    }
  }
}