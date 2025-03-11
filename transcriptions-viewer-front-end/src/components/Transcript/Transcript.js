import TranscriptItem from '../TranscriptItem/TranscriptItem';

export default function Transcript(props) {
  const {
    lines,
    onClickLine
  } = props;

  if (lines === undefined || lines === null || lines.length === 0) {
    return (<></>)
  }

  return (
    <>
      {lines.map((e) => <TranscriptItem key={e.id}  item={e} onClickItem={onClickLine} />)}
    </>
  )
}