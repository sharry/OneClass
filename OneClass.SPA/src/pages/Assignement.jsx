import React, { useState } from "react";
import Navbar from "../components/Navbar";
import TodoCard from "../components/TodoCard";
import { Link, useParams } from "react-router-dom";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
import { getClassAssignments } from "../api/class";
import { useQuery } from "@tanstack/react-query";

function Assignement() {
  const { id } = useParams();
  const [assignments, setAssignments] = useState([]);
  const { isLoading } = useQuery(
    ["classAssignment", id],
    getClassAssignments,
    {
      onSuccess: setAssignments,
    }
  );
  if (isLoading) return "Loading...";

  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="assignment" id={id} />
      {assignments.map((assignment) => (
        <TodoCard
          key={assignment.id}
          title={assignment.title}
          classroom={assignment.classroom}
          hasDueDate={assignment.hasDueDate}
          dueDate={assignment.dueDate}
        />
      ))}
    </RequireAuth>
  );
}

export default Assignement;
