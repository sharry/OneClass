import React from "react";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
import CourseCard from "../components/CourseCard";
import RequireAuth from "../components/RequireAuth";
import { useQuery } from "@tanstack/react-query";
import { getClasses } from "../api/class";

function Home() {
  const { isLoading, data } = useQuery({
    queryKey: ["classes"],
    queryFn: getClasses,
  });

  if (isLoading) return "Loading...";

  const Classes = data;

  return (
    <RequireAuth>
      <div className="h-full">
        <Navbar />
        {Classes.length > 0 ? (
          <div className="pb-20">
            {Classes.map((course) => (
              <CourseCard
                key={course.id}
                id={course.id}
                name={course.title}
                teacher={course.teacher}
                subject={course.description}
                todos={course.assignmentsCount}
                students={course.studentIds}
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
