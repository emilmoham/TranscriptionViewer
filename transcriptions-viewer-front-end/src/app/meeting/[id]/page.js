'use client';
import { useParams } from 'next/navigation'
import { Player } from "webvtt-player"

export default function Meeting(props) {
  const params = useParams();

  return (
    <div>
      <p>{params.id}</p>
      <Player 
        audio="todo"
        transcript="todo" 
      />
    </div>
  );
} 