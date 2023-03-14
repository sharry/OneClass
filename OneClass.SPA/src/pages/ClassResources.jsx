import BottomTabs from "../components/BottomTabs";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";
import { Link } from "react-router-dom";

BottomTabs;

function ClassRessources() {
  return (
    <>
      <Navbar />
      <div className="grid grid-cols-3 gap-4
       justify-around w-full px-7">
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
        <Link to="/classrources">Content</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
            <Link to="assignment">Assignments</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
          Members
        </button>
      </div>
      <ClassRessourcesCard/>
      <ClassRessourcesCard/>
      <ClassRessourcesCard/>
    </>
  );
}

export default ClassRessources;
