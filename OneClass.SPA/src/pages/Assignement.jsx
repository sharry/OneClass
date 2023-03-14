import React from "react";
import Navbar from "../components/Navbar";
import TodoCard from "../components/TodoCard";
import { Link, useParams } from "react-router-dom";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";

function Assignement() {
  const { id } = useParams();

  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="assignment" id={id} />
      <TodoCard />
      <TodoCard />
      <TodoCard />
      <TodoCard />
    </RequireAuth>
  );
}

export default Assignement;
