import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
import TodoCard from "../components/TodoCard";


function NotificationPage() {
  return (
    <div className="h-full">
      <Navbar /> 
      <div className="flex justify-around">
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">Assigned</button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">Missed</button>
        <button className="py-3 px-6 bg-green-600 text-white rounded-3xl w-28 my-6">Done</button>
      </div>
      <TodoCard/>
      <TodoCard/>
      <TodoCard/>
      <TodoCard/>
      <BottomTabs />
    </div>
  );
}

export default NotificationPage;
