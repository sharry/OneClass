import { useQuery } from "@tanstack/react-query";
import { useParams } from "react-router-dom";
import { getAssignment } from "../api/class";
import AttachementCard from "../components/AttachementCard";
import Navbar from "../components/Navbar";
import RequireAuth from "../components/RequireAuth";
import dayjs from "dayjs";

function AssignmentContent(){
    const { id } = useParams();
    const { isLoading, data } = useQuery({
        queryKey: ["classAssignment", id],
        queryFn: getAssignment,
    });

    if (isLoading) return "Loading...";

    const assignment = data || [];

    console.log(assignment)
  
    return <RequireAuth>
        <Navbar/>
        <div className="mx-3">
        <h1 className="text-2xl mb-3 mt-6 font-medium">{assignment.title}</h1>
        {assignment.hasDueDate && <p className="mb-2 mt-3 text-gray-400">{dayjs(assignment.dueDate).fromNow()}</p>}
        <hr className="mb-6" />
             <p className=" mb-6">{assignment.content}</p>
             <button className="bg-green-600 w-11/12 mx-auto block text-white py-2 rounded-lg mt-8">Add Work</button>
        </div>
    </RequireAuth>
}

export default AssignmentContent;