'use client';
import { useState, useEffect } from 'react';
import MeetingCard from '../MeetingCard/MeetingCard';

import styles from './MeetingList.module.css';

export default function MeetingList(props) {
  const {
    isLoading,
    data
  } = props;

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