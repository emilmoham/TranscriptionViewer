import Link from 'next/link';
import Markdown from 'react-markdown';
import moment from 'moment';
import TranscriptItem from '../TranscriptItem/TranscriptItem';
import styles from './MeetingCard.module.css';

export default function MeetingCard(props) {
  const {
    meetingData
  } = props;

  const {
    id,
    title,
    meetingDate,
    summary, 
    transcriptItems
  } = meetingData;

  let date = new Date(meetingDate);
  date = moment(date).format('MMMM D, YYYY');

  return (
    <div className={styles.MeetingCard}>
      <div className={styles.MeetingInfoContainer}>
        <h1><Link href={`/meeting/${id}`}>{title}</Link></h1>
        <h3>{date}</h3>
        <Markdown>{summary}</Markdown>
        { transcriptItems &&
          transcriptItems.map((e) => <TranscriptItem key={e.id}  item={e} />)
        }
      </div>
    </div>
  );
}