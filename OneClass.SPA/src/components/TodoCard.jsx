import React from "react";
import { Link } from "react-router-dom";

function TodoCard({ id, title, classroom, hasDueDate, dueDate }) {
	return (
    <>
      <div className="flex justify-between items-center px-4 py-2 border-b-2 mx-2">
        <div className="flex items-center gap-4">
          <img
            alt=""
            src="https://source.unsplash.com/100x100/?portrait"
            className="ml-3 object-cover w-16 h-16 rounded-full shadow dark:bg-gray-500"
          />
          <div className="flex flex-col ml-4">
            <h1 className="font-medium text-lg">
              <Link to={`/assignmentcontent/${id}`}>{title}</Link>
            </h1>
            <p className="text-gray-500">Classroom name</p>
          </div>
        </div>
        {hasDueDate && (
          <div className="flex flex-col text-red-600 font-medium">
            <p>Yesterday</p>
            <p>23:00</p>
          </div>
        )}
      </div>
    </>
  );
}
export default TodoCard;
