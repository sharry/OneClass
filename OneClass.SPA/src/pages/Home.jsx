import Navbar from "../components/Navbar";
import CourseCard from "../components/CourseCard";
import BottomTabs from "../components/BottomTabs"

function Home() {
  return (
    <div className="">
      <Navbar /> <CourseCard /> <CourseCard /> <CourseCard /><CourseCard />
      <BottomTabs/>
    </div>
  );
}

export default Home;
