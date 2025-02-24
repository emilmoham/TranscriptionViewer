using Microsoft.EntityFrameworkCore;
using TranscriptionsViewerApi.Models.Entities;

namespace TranscriptionsViewerApi.Repositories 
{
  public interface ITranscriptionsRepository {
    Task<IEnumerable<Meeting>> GetMeetings(
      DateTimeOffset? rangeStart = null,
      DateTimeOffset? rangeEnd = null,
      int limit = 10);
  }

  public class TranscriptionsRepository : ITranscriptionsRepository 
  {
    private readonly ApplicationContext _applicationContext;

    public TranscriptionsRepository(ApplicationContext applicationContext)
    {
      _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<Meeting>> GetMeetings(
      DateTimeOffset? rangeStart = null,
      DateTimeOffset? rangeEnd = null,
      int limit = 10) 
    {
      if (rangeStart == null)
      {
        rangeStart = DateTimeOffset.UtcNow.AddMonths(-1);
      }

      if (rangeEnd == null) 
      {
        rangeEnd = DateTimeOffset.UtcNow;
      }

      return await _applicationContext.Meetings
        .Where(e => e.MeetingDate > rangeStart 
          && e.MeetingDate < rangeEnd)
        .Take(limit)
        .ToListAsync();
    }
  }
}