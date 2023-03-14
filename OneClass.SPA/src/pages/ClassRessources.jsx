import React from "react";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import RequireAuth from "../components/RequireAuth";
import RessourcesNavigator from "../components/RessourcesNavigator";
// id, teacher, dateTime, content, attchementsNbr;
const contentList = [
  {
    id: 1,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "1976-04-19T12:59-0500",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 2,
  },
  {
    id: 2,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "1976-04-19T12:59-0500",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 2,
  },
  {
    id: 3,
    teacher: {
      name: "Elkhiat Brahim",
      image: "https://source.unsplash.com/100x100/?portrait",
    },
    dateTime: "1976-04-19T12:59-0500",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    attchementsNbr: 0,
  },
];

function ClassRessources() {
  return (
    <RequireAuth>
      <Navbar />
      <RessourcesNavigator active="content" />
      {contentList.map((content) => (
        <ClassRessourcesCard
          key={content.id}
          teacher={content.teacher}
          dateTime={content.dateTime}
          content={content.content}
          attchementsNbr={content.attchementsNbr}
        />
      ))}
    </RequireAuth>
  );
}

export default ClassRessources;
