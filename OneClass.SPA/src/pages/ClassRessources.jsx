import React from "react";
import BottomTabs from "../components/BottomTabs";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";

BottomTabs;

function ClassRessources() {
  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="content" />
      <ClassRessourcesCard />
      <ClassRessourcesCard />
      <ClassRessourcesCard />
    </RequireAuth>
  );
}

export default ClassRessources;
