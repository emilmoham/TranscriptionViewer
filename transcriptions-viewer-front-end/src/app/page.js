'use client';
import { useEffect, useState } from 'react';
import { useRouter, useSearchParams } from 'next/navigation';
import DebouncedTextInput from '@/components/DebouncedTextInput/DebouncedTextInput';
import MeetingList from "@/components/MeetingList/MeetingList";

export default function Home() {  
  const [data, setData] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [debouncedQuery, setDebouncedQuery] = useState('');

  const router = useRouter(); 

  const searchParams = useSearchParams();
  const search = searchParams.get('search');

  useEffect(() => {
    if (search != debouncedQuery)
      setDebouncedQuery(search ?? '');

    let url = `http://localhost:5065/Transcriptions/Meetings`;
    if (search) url +=  `/Search?query=${search}`

    setLoading(true);
    fetch(url)
    .then((res) => res.json())
    .then((data) => {
      setData(data);
      setLoading(false);
    });
  }, [search])

  useEffect(() => {
    if (debouncedQuery.length >= 2 ) {
      router.push(`?${new URLSearchParams({search: debouncedQuery})}`);
    } else {
      router.push(`?`);
    }
  }, [router, debouncedQuery])


  return (
    <main>
      <DebouncedTextInput 
        value={debouncedQuery}
        onChange={(value) => setDebouncedQuery(value)}
        placeholder='Search' />
      <MeetingList data={data} />
    </main>
  );
}
