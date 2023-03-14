import { useEffect, useState } from "react";

function useRefresh() {
  const [refresh, setRefresh] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setRefresh((prev) => prev + 1);
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  return refresh;
}

export default useRefresh;
