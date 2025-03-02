using Microsoft.EntityFrameworkCore;
using TranscriptionsViewerApi.Models.DTOs;
using TranscriptionsViewerApi.Models.Entities;

namespace TranscriptionsViewerApi.Repositories 
{
  public interface ITranscriptionsRepository {
    
    void AddMeeting(Meeting meeting);

    void AddMeetings(IEnumerable<Meeting> meetings);
    
    Task<Meeting?> GetMeeting(int id);

    Task<IEnumerable<Meeting>> GetMeetings(
      DateTimeOffset? rangeStart = null,
      DateTimeOffset? rangeEnd = null,
      int limit = 10);

    Task<IEnumerable<RankedMeeting>> QueryMeetings(string searchTerm);

    Task<int> SaveChangesAsync();
  }

  public class TranscriptionsRepository : ITranscriptionsRepository 
  {
    private readonly ApplicationContext _applicationContext;

    public TranscriptionsRepository(ApplicationContext applicationContext)
    {
      _applicationContext = applicationContext;
    }

    public void AddMeeting(Meeting meeting) 
    {
      _applicationContext.Meetings.Add(meeting);
    }

    public void AddMeetings(IEnumerable<Meeting> meetings)
    {
      _applicationContext.Meetings.AddRange(meetings);
    }

    public async Task<Meeting?> GetMeeting(int id) {
      return await _applicationContext.Meetings.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Meeting>> GetMeetings(
      DateTimeOffset? rangeStart = null,
      DateTimeOffset? rangeEnd = null,
      int limit = 10) 
    {
      IQueryable<Meeting> meetings = _applicationContext.Meetings.AsQueryable();

      if (rangeStart != null)
      {
        meetings = meetings.Where(e => e.MeetingDate >= rangeStart);
      }

      if (rangeEnd != null) 
      {
        meetings = meetings.Where(e => e.MeetingDate <= rangeEnd);
      }

      return await _applicationContext.Meetings
        .OrderBy(e => e.MeetingDate)
        .Reverse()
        .Take(limit)
        .ToListAsync();
    }

    public async Task<IEnumerable<RankedMeeting>> QueryMeetings(string searchTerm) 
    {
      var query = EF.Functions.PhraseToTsQuery(searchTerm);

      return await _applicationContext.TranscriptItems
        .Include(e => e.Meeting)
        .Where(e => 
          e.SearchVector.Matches(query)
          || e.Meeting.SearchVector.Matches(query)
        ).GroupBy(e => e.Meeting)
        .Select(m => new RankedMeeting {
          Id = m.Key.Id,
          Title = m.Key.Title,
          MeetingDate = m.Key.MeetingDate,
          RecordingKey = m.Key.RecordingKey,
          CaptionsKey = m.Key.CaptionsKey,
          Summary = m.Key.Summary,
          TranscriptItems = m.ToList(),
          Rank = m.Key.SearchVector.Rank(EF.Functions.PhraseToTsQuery(searchTerm)) // TODO include rank from individual meeting lines
        }).ToListAsync();
    }

    public async Task<int> SaveChangesAsync() {
      return await _applicationContext.SaveChangesAsync();
    }
  }
}