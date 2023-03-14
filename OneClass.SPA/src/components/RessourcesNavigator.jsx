import React from "react";
import { Link } from "react-router-dom";
const RessourcesNavigator = ({ active }) => {
  const activeStyle =
    "bg-green-600 text-white rounded-3xl my-6 border-2 border-gray-500";
  const inactiveStyle =
    "p-3  bg-gray-400 text-white rounded-3xl my-6 border-2 border-gray-500";
  return (
    <div
      className="grid grid-cols-3 gap-3
       justify-around w-full px-3"
    >
      <button className={active == "content" ? activeStyle : inactiveStyle}>
        <Link to="/classrources">Content</Link>
      </button>
      <button className={active == "assignment" ? activeStyle : inactiveStyle}>
        <Link to="/classrources/assignment">Assignments</Link>
      </button>
      <button className={active == "members" ? activeStyle : inactiveStyle}>
        <Link to="/classrources/members">Members</Link>
      </button>
    </div>
  );
};

export default RessourcesNavigator;
