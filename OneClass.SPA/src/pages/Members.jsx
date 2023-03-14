import React from "react";
import Navbar from "../components/Navbar";
import MemberCard from "../components/MemberCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { getClassMembers } from "../api/class";

function Members() {
  const { id } = useParams();
  const { isLoading, data } = useQuery({
    queryKey: ["classes", id],
    queryFn: getClassMembers,
  });

  if (isLoading) return "Loading...";

  const members = data;
  console.log(members);

  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="members" id={id} />
      <h1 className="ml-5 font-semibold text-lg pb-3">Teacher:</h1>
      <div className="flex items-center bg-green-200 py-3 rounded-xl mx-4 border-2 border-green-500">
        <img
          alt=""
          src="https://source.unsplash.com/100x100/?portrait"
          className="ml-3 object-cover w-14 h-14 rounded-full shadow dark:bg-gray-500"
        />
        <p className="ml-4 font-medium">{members.teacher.displayName}</p>
      </div>
      {members.students.length > 0 && (
        <div>
          <h1 className="ml-5 font-semibold text-lg pb-2 mt-10">Students:</h1>
          <div className="bg-gray-100 py-1 rounded-xl mx-4 border-2 border-gray-400 divide-y divide-gray-300">
            {members.students.map((student) => (
              <MemberCard
                key={student.id}
                id={student.id}
                name={student.displayName}
                image={student.image}
              />
            ))}
          </div>
        </div>
      )}
    </RequireAuth>
  );
}

export default Members;
