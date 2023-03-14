import React, { useState } from "react";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
import { useParams } from "react-router-dom";
import { getClassResources } from "../api/class";
import { useQuery } from "@tanstack/react-query";
// id, teacher, dateTime, content, attchementsNbr;

function ClassRessources() {
  // on component mount, fetch the class resources using useQuery
  const { id } = useParams();
  const { isLoading, error, data } = useQuery(
    ["classResources", id],
    getClassResources
  );
  const [contentList, setContentList] = useState([]);
  if (isLoading) return "Loading...";
  if (error) return "An error has occurred: " + error.message;

  if (data) {
    setContentList(data);
  }

  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="content" id={id} />
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
