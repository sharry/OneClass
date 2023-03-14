import React from "react";
import Navbar from "../components/Navbar";
import TodoCard from "../components/TodoCard";
import { Link } from "react-router-dom";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";

function Assignement() {
  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="assignment" />
      <TodoCard />
      <TodoCard />
      <TodoCard />
      <TodoCard />
    </RequireAuth>
  );
}

export default Assignement;
