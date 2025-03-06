import MeetingCard from '../MeetingCard/MeetingCard';

import styles from './MeetingList.module.css';

export default function MeetingList(props) {
  const { data } = props;

  return (
    <div className={styles.MeetingList}>
      {data.map((m) => {
        return (<MeetingCard key={m.id} meetingData={m} />)
      })}
    </div>
  );
}