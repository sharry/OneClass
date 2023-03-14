import Navbar from "../components/Navbar";
import { Link } from "react-router-dom";
import MemberCard from "../components/MemberCard";
import RequireAuth from "../components/RequireAuth";

function Members() {
  return (
    <RequireAuth>
      <Navbar />
      <div className="grid grid-cols-3 gap-4
       justify-around w-full px-7">
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
        <Link to="/classrources">Content</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
            <Link to="/classrources/assignment">Assignments</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
        <Link to="members">Members</Link>
        </button>
      </div>
      <h1 className="ml-5 font-semibold text-lg pb-3">Teacher:</h1>
      <div className="flex items-center bg-green-200 py-3 rounded-xl mx-4 border-2 border-green-500">
        <img
          alt=""
          src="https://source.unsplash.com/100x100/?portrait"
          className="ml-3 object-cover w-14 h-14 rounded-full shadow dark:bg-gray-500"
        />
        <p className="ml-4 font-medium">Elkhiat Brahim</p>
      </div>
      <h1 className="ml-5 font-semibold text-lg pb-2 mt-10">Students:</h1>
      <div className="bg-gray-100 py-1 rounded-xl mx-4 border-2 border-gray-400 divide-y divide-gray-300">
        <MemberCard/>
        <MemberCard/>
        <MemberCard/>
        <MemberCard/>
      
      </div>
      
    </RequireAuth>
  );
}

export default Members;
