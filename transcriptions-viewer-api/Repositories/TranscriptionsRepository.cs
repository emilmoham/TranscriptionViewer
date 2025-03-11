using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
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
      Meeting? meeting = await _applicationContext.Meetings
        .Include(e => e.TranscriptItems)
        .FirstOrDefaultAsync(e => e.Id == id);
      
      if (meeting != null)
        meeting.TranscriptItems = meeting.TranscriptItems.OrderBy(e => e.TimestampStart);

      return meeting;
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
      return await _applicationContext.Meetings
        .Include(e => e.TranscriptItems)
        .Where(e => 
            e.TitleSearchVector
              .Concat(e.SummarySearchVector)
              .Matches(EF.Functions.WebSearchToTsQuery(searchTerm))
          ||e.Id == _applicationContext.TranscriptItems
              .Where(t => t.SearchVector.Matches(EF.Functions.WebSearchToTsQuery(searchTerm)))
              .Select(t => t.MeetingId)
              .FirstOrDefault())
        .Select(e => new RankedMeeting {
          Id = e.Id,
          Title = e.Title,
          MeetingDate = e.MeetingDate,
          RecordingKey = e.RecordingKey,
          CaptionsKey = e.CaptionsKey,
          Summary = e.Summary,
          Rank = e.TitleSearchVector
            .Concat(e.SummarySearchVector)
            .Rank(EF.Functions.WebSearchToTsQuery(searchTerm)),
          TranscriptItems = e.TranscriptItems
            .Where(t => t.SearchVector.Matches(EF.Functions.WebSearchToTsQuery(searchTerm)))
            .Select(t => new RankedTranscriptItem {
              Id = t.Id,
              MeetingId = t.MeetingId,
              TimestampStart = t.TimestampStart,
              TimestampEnd = t.TimestampEnd,
              Text = t.Text,
              Rank = t.SearchVector.Rank(EF.Functions.WebSearchToTsQuery(searchTerm))
            }).AsEnumerable()
        }).ToListAsync();
    }

    public async Task<int> SaveChangesAsync() {
      return await _applicationContext.SaveChangesAsync();
    }
  }
}