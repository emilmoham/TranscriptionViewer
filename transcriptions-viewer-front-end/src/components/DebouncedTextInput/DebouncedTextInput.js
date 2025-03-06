'use client';
import { useCallback, useEffect, useState } from 'react';
import styles from "./DebouncedTextInput.module.css"

export default function DebouncedTextInput({
  value,
  onChange,
  debounceTime = 500,
  placeholder = '',
  ...inputProps
}) {
  const [localValue, setLocalValue] = useState(value);
  
  useEffect(() => {
    setLocalValue(value);
  }, [value]);

  const debouncedOnChange = useCallback(
    (value) => {
      const handler = setTimeout(() => {
        onChange(value);
      }, debounceTime);
      return () => {
        clearTimeout(handler);
      };
    },
    [onChange, debounceTime]
  );

  useEffect(() => {
    // Only trigger if the local value is different from the controlled value
    if (localValue !== value) {
      const cleanup = debouncedOnChange(localValue);
      return cleanup;
    }
  }, [localValue, value, debouncedOnChange]);

  const handleChange = (e) => {
    setLocalValue(e.target.value);
  };

  return (
      <div className={styles.search}>
        <input
          type="text"
          value={localValue}
          onChange={handleChange}
          placeholder={placeholder}
          {...inputProps} />
      </div>
  );
}