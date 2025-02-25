import Link from 'next/link';
import Markdown from 'react-markdown';
import moment from 'moment';
import styles from './MeetingCard.module.css';

export default function MeetingCard(props) {
  const {
    meetingData
  } = props;

  const {
    id,
    title,
    meetingDate,
    summary
  } = meetingData;

  let date = new Date(meetingDate);
  console.log(date);
  date = moment(date).format('MMMM D, YYYY');

  return (
    <div className={styles.MeetingCard}>
      <h3><Link href={`/meeting/${id}`}>{title}</Link></h3>
      <h5>{date}</h5>
      <Markdown>{summary}</Markdown>
    </div>
  );
}