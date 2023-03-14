import BottomTabs from "../components/BottomTabs";
import Navbar from "../components/Navbar";
import ClassRessourcesCard from "../components/ClassResourcesCard";

BottomTabs;

function ClassRessources() {
  return (
    <>
      <Navbar />
      <div className="flex justify-around">
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">
          Assigned
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">
          Missed
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">
          Done
        </button>
      </div>
      <ClassRessourcesCard/>
      <ClassRessourcesCard/>
      <ClassRessourcesCard/>
    </>
  );
}

export default ClassRessources;
