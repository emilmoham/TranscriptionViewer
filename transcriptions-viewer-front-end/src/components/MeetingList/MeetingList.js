'use client';
import { useState, useEffect } from 'react';
import MeetingCard from '../MeetingCard/MeetingCard';

import styles from './MeetingList.module.css';

export default function MeetingList() {
  const [data, setData] = useState(null);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    fetch('http://localhost:5065/Transcriptions/Meetings')
    .then((res) => res.json())
    .then((data) => {
      setData(data);
      setLoading(false);
    });
  }, [])

  useEffect(() => {
    console.log(data);
  }, [data])

  if (isLoading) {
    return (<div>Loading</div>);
  }

  return (
    <div className={styles.MeetingList}>
      {data.map((m) => {
        return (<MeetingCard key={m.id} meetingData={m} />)
      })}
    </div>
  );
}