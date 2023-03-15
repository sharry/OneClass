import React from "react";
import { Link } from "react-router-dom";

function FAB() {
  const [isOpen, setIsOpen] = React.useState(false);

  return (
    <div className="fixed bottom-[120px] right-4 flex flex-col items-end">
      {isOpen && (
        <ul>
          <li className="mb-2 bg-white border px-4 py-2 rounded font-medium cursor-pointer hover:bg-gray-200">
            <Link to="/create">Add Classroom</Link>
          </li>
          <li className="mb-2 bg-white border px-4 py-2 rounded font-medium cursor-pointer hover:bg-gray-200">
            <Link to="/join">Join Classroom</Link>
          </li>
        </ul>
      )}
      <button
        class="p-0 w-14 h-14 bg-white border rounded-full hover:bg-gray-200 mouse shadow transition ease-in duration-200"
        onClick={() => setIsOpen(!isOpen)}
      >
        <svg
          viewBox="0 0 20 20"
          enable-background="new 0 0 20 20"
          class="w-6 h-6 inline-block"
        >
          <path
            fill="#444444"
            d="M16,10c0,0.553-0.048,1-0.601,1H11v4.399C11,15.951,10.553,16,10,16c-0.553,0-1-0.049-1-0.601V11H4.601
                                    C4.049,11,4,10.553,4,10c0-0.553,0.049-1,0.601-1H9V4.601C9,4.048,9.447,4,10,4c0.553,0,1,0.048,1,0.601V9h4.399
                                    C15.952,9,16,9.447,16,10z"
          />
        </svg>
      </button>
    </div>
  );
}

export default FAB;
