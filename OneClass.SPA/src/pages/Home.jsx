import React from "react";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs"
import CourseCard from "../components/CourseCard";


function Home() {
  return (
    <div className="h-full">
      <Navbar /> <CourseCard /> <CourseCard /><CourseCard /><CourseCard />
      <BottomTabs/>
    </div>
  );
}

export default Home;
