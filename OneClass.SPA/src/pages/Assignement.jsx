import Navbar from "../components/Navbar";
import TodoCard from "../components/TodoCard";
import { Link } from "react-router-dom";
import RequireAuth from "../components/RequireAuth";

function Assignement(){
    return <RequireAuth>
    <Navbar/>
    <div className="grid grid-cols-3 gap-4
       justify-around w-full px-7">
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
        <Link to="/classrources">Content</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
            <Link to="assignment">Assignments</Link>
        </button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl my-6">
        <Link to="/classrources/members">Members</Link>
        </button>
      </div>
        <TodoCard/>
        <TodoCard/>
        <TodoCard/>
        <TodoCard/>
    </RequireAuth>
}

export default Assignement;