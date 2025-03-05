using TranscriptionsViewerApi.Models.DTOs;
using TranscriptionsViewerApi.Models.Entities;
using TranscriptionsViewerApi.Repositories;

namespace TranscriptionsViewerApi.Services 
{
  public class TranscriptionsService 
  {
    private readonly ITranscriptionsRepository _transcriptionsRepository;

    public TranscriptionsService (
      ITranscriptionsRepository transcriptionsRepository
    ) {
      _transcriptionsRepository = transcriptionsRepository;
    }

    public async Task<IEnumerable<Meeting>> GetMeetings() {
      return await _transcriptionsRepository.GetMeetings();
    }

    public async Task<Meeting?> GetMeetingById(int id) {
      return await _transcriptionsRepository.GetMeeting(id);
    }

    public async Task<IEnumerable<RankedMeeting>> QueryMeetings(string query) {
      return await _transcriptionsRepository.QueryMeetings(query);
    }
  }
}