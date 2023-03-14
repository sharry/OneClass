import React from "react";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
import CourseCard from "../components/CourseCard";
import RequireAuth from "../components/RequireAuth";

const Classes = [
  {
    id: 0,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 1,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 2,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 3,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 4,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 5,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 6,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 7,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
  {
    id: 8,
    name: "Windows Server 2022",
    teacher: {
      name: "Said Naji",
      image: "https://source.unsplash.com/50x50/?portrait",
    },
    subject: "System Administration",
    todos: 12,
    students: 40,
    image: "https://source.unsplash.com/50x50/?portrait",
  },
];

function Home() {
  return (
    <RequireAuth>
      <div className="h-full">
      <Navbar />
      {Classes.length > 0 ? (
        <div className="pb-20">
          {Classes.map((course) => (
            <CourseCard
              key={course.id}
              name={course.name}
              teacher={course.teacher}
              subject={course.subject}
              todos={course.todos}
              students={course.students}
              image={course.image}
            />
          ))}
        </div>
      ) : (
        <div className="flex flex-col justify-center items-center">
          <img src="../assets/classNotFound.png" alt="" />
          <h1 className="text-2xl font-semibold text-gray-500">
            No Class Room Found
          </h1>
          <p className="text-gray-500">
            Please{" "}
            <a href="" className="text-green-600 underline">
              create a class
            </a>{" "}
            or{" "}
            <a href="" className="text-green-600 underline">
              join existing
            </a>{" "}
            one
          </p>
        </div>
      )}

        <BottomTabs />
      </div>
    </RequireAuth>
  );
}

export default Home;
