import AttachementCard from "../components/AttachementCard";
import Navbar from "../components/Navbar";
import RequireAuth from "../components/RequireAuth";

function AssignmentContent(){
    return <RequireAuth>
        <Navbar/>
        <div className="mx-3">
        <h1 className="text-2xl mt-6 font-medium">Reading Activity - Properties</h1>
        <p className="mb-2 mt-3 text-gray-400">Due Tomorrow</p>
        <hr />
        <p className="my-4">Lorem ipsum dolor sit, amet consectetur adipisicing elit. Ullam temporibus iure veritatis praesentium minus commodi, officia facere?
             </p>
             <p className=" mb-6">Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deleniti natus eos possimus dolorem, delectus nobis reprehenderit in!</p>
             <AttachementCard/>
             <AttachementCard/>
             <button className="bg-green-600 w-11/12 mx-auto block text-white py-2 rounded-lg mt-8">Add Work</button>
        </div>
    </RequireAuth>
}

export default AssignmentContent;