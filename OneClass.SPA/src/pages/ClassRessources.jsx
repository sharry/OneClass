import React, { useState } from "react";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
import { useParams } from "react-router-dom";
import { getClassResources } from "../api/class";
import { useQuery } from "@tanstack/react-query";
import AddResource from "../components/AddResource";
// id, teacher, dateTime, content, attchementsNbr;

function ClassRessources() {
  const { id } = useParams();
  const { isLoading, data } = useQuery({
    queryKey: ["classResources", id],
    queryFn: getClassResources,
  });

  if (isLoading) return "Loading...";

  const contentList = data || [];

  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="content" id={id} />
      <AddResource />
      {contentList.map((content) => (
        <ClassRessourcesCard
          key={content.id}
          teacher={content.teacher}
          dateTime={content.dateTime}
          content={content.content}
          attchementsNbr={content.attachments?.length}
        />
      ))}
    </RequireAuth>
  );
}

export default ClassRessources;
