import React from "react";
import { Link } from "react-router-dom";

function ClassRessourcesCard({ teacher, dateTime, content, attchementsNbr }) {
  return (
    <>
      <div className="bg-slate-200 p-4 rounded-lg border-2 mb-4">
        <div className="flex py-2">
          <img
            alt=""
            src={teacher?.image}
            className="ml-3 object-cover w-12 h-12 rounded-full shadow dark:bg-gray-500 mr-2"
          />
          <div>
            <h3>
              {teacher?.name}
              <div className="22">
                <div className=""></div>
              </div>
            </h3>
            <p>2 days ago</p>
          </div>
        </div>
        <p className="text-xl px-6 py-2">{content}</p>
        {attchementsNbr > 0 && (
          <button className="ml-6 py-2 px-4 bg-slate-100 rounded-2xl border-2 mt-1 flex justify-between">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M18.375 12.739l-7.693 7.693a4.5 4.5 0 01-6.364-6.364l10.94-10.94A3 3 0 1119.5 7.372L8.552 18.32m.009-.01l-.01.01m5.699-9.941l-7.81 7.81a1.5 1.5 0 002.112 2.13"
              />
            </svg>
            <Link to="classcontent">
              <span className="ml-2">{attchementsNbr} attachements</span>
            </Link>
          </button>
        )}
      </div>
    </>
  );
}

export default ClassRessourcesCard;
