import React from "react";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
import { useParams } from "react-router-dom";
import { getClassResources } from "../api/class";
import { useQuery } from "@tanstack/react-query";
// id, teacher, dateTime, content, attchementsNbr;
const contentList = [
  {
    id: 1,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "2021-05-20 12:00:00",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 2,
  },
  {
    id: 2,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "2021-05-20 12:00:00",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 2,
  },
  {
    id: 3,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "2021-05-20 12:00:00",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 0,
  },
];

function ClassRessources() {
  const { id } = useParams();
  const { isLoading, data } = useQuery({
    queryKey: ["classes", id],
    queryFn: getClassResources,
  });

  if (isLoading) return "Loading...";

  const contentList = data;

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
