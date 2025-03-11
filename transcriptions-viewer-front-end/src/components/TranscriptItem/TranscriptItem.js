export default function TranscriptItem(props) {
  const {
    item,
    onClickItem,
  } = props;

  const {
    meetingId,
    timestampStart,
    text,
  } = item;

  const convertTimestampToString = (duration) => {
    let milliseconds = Math.floor((duration % 1000)),
      seconds = Math.floor((duration / 1000) % 60),
      minutes = Math.floor((duration / (1000 * 60)) % 60),
      hours = Math.floor((duration / (1000 * 60 * 60)) % 24);

    let output = '';
    output += String(hours).padStart(2, '0');
    output += ':';    
    output += String(minutes).padStart(2, '0');
    output += ':';
    output += String(seconds).padStart(2, '0');
    output += '.';
    output += String(milliseconds).padStart(3, '0');
    return output;
  }

  return (
    <div>
      <div onClick={() => onClickItem(timestampStart)}>{convertTimestampToString(timestampStart)}</div>
      <div>{text}</div>
    </div>
  )
}