'use client';
import { useEffect, useState } from 'react';
import styles from "./DebouncedTextInput.module.css"

export default function DebouncedTextInput(props) {
  const { setDebouncedQuery } = props;

  const [query, setQuery] = useState("");
  
  const handleChange = (event) => {
    setQuery(event.target.value);
  };

  useEffect(() => {
    const delayInputTimeoutId = setTimeout(() => {
      setDebouncedQuery(query);
    }, 500);
    return () => clearTimeout(delayInputTimeoutId);
  }, [query, setDebouncedQuery]);

  return (
      <div className={styles.search}>
        <input
          type="text"
          id="query"
          onChange={handleChange}
          placeholder="Search" />
      </div>
  );
}