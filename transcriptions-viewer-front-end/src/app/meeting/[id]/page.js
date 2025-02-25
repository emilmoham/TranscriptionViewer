'use client';
import { useParams } from 'next/navigation'
import { useEffect, useState } from 'react';
import { Player } from "webvtt-player"

export default function Meeting(props) {
  const params = useParams();

  const [meeting, setMeeting] = useState(null);

  const apiBase = "http://localhost:5065"; // TODO: change this url base to env value
  const cdnBase = "http://localhost:19002"; // TODO: change this url base to env value

  useEffect(() => {
    async function fetchMeeting(id) {
      const res = await fetch(`${apiBase}/transcriptions/meeting/${id}`); 
      const data = await res.json();
      setMeeting(data);
    }
    fetchMeeting(params.id);
  }, [])

  if (!meeting) return <div>Loading...</div>

  return (
    <div>
      <h3>{meeting.title}</h3>
      <Player 
        audio={`${cdnBase}/${meeting.recordingKey}`} 
        transcript={`${cdnBase}/${meeting.captionsKey}`} 
      />
    </div>
  );
} 