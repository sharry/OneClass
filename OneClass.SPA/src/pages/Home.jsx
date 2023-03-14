import React from "react";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs"
import CourseCard from "../components/CourseCard";

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
  }, {
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
  }, {
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
  }, {
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
  }, {
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
  }, {
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
]

function Home() {
  return (
    <div className="h-full">
      <Navbar />
      <div className="pb-20">
        {Classes.map((course) => (
          <CourseCard key={course.id} name={course.name} teacher={course.teacher} subject={course.subject} todos={course.todos} students={course.students} image={course.image} />
        ))}
      </div>

      <BottomTabs />
    </div>
  );
}

export default Home;
