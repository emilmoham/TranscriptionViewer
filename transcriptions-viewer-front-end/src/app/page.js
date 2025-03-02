import MeetingList from "@/components/MeetingList/MeetingList";
import styles from "./app.module.css"

export default function Home() {
  return (
    <main>
      <div className={styles.search}>
        <input type="text" id="query" placeholder="Search"/>
      </div>
      <div>
        <MeetingList />
      </div>
    </main>
  );
}
