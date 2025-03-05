'use client';
import { useEffect, useState } from 'react';
import DebouncedTextInput from '@/components/DebouncedTextInput/DebouncedTextInput';
import MeetingList from "@/components/MeetingList/MeetingList";

export default function Home() {  
  const [data, setData] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [debouncedQuery, setDebouncedQuery] = useState('');

  useEffect(() => {
    fetch('http://localhost:5065/Transcriptions/Meetings')
    .then((res) => res.json())
    .then((data) => {
      setData(data);
      setLoading(false);
    });
  }, [])

  useEffect(() => {
    if (debouncedQuery.length > 2 ) {
      setLoading(true);
      fetch(`http://localhost:5065/Transcriptions/Meetings/Search?query=${debouncedQuery}`)
        .then((res) => res.json())
        .then((data) => {
          setData(data);
          setLoading(false);
        });
    }
  }, [debouncedQuery])


  return (
    <main>
      <DebouncedTextInput setDebouncedQuery={setDebouncedQuery}/>
      <MeetingList
        isLoading={isLoading}
        data={data} />
    </main>
  );
}
