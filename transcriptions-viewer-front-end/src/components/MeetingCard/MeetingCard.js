import Markdown from 'react-markdown';
import styles from './MeetingCard.module.css';

export default function MeetingCard(props) {
  const {
    meetingData
  } = props;

  const {
    title,
    meetingDate,
    summary
  } = meetingData;

  return (
    <div className={styles.MeetingCard}>
      <h3>{title}</h3>
      <h5>{meetingDate}</h5>
      <Markdown>{summary}</Markdown>
    </div>
  );
}