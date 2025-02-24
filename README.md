# Transcription Viewer

This project attempts to give a user interface for storing and querying meeting
transcriptions.

## Transcription Workflow

1. Host and Record a meeting
2. Use [OpenAI/Whisper](todo) to create a transcription of the meeting
    * We need a vtt file as we as a csv/tsv of the transcription
3. Use [Mixtral-Nemo](todo) on the csv/tsv file get a summary of the transcription
4. Create a meeting record in the database with the following fields, making
note of the new meeting id
    * Title
    * Date
    * Summary
5. Use the CSV or TSV file to create TranscriptionItem records with the
following fields:
    * Meeting_Id
    * Starting Timestamp
    * Ending Timestamp
    * Text

## Development Setup

1. Clone the repository and navigate to the project root in a terminal
2. Start the service dependencies with `docker compose up -d`
3. Restore packages with `dotnet restore`
4. Naviate to the backend project with `cd TranscriptionsViewerApi`
5. Run the database migrations with `dotnet ef database update`
6. Build and run the application in development mode `dotnet run`
