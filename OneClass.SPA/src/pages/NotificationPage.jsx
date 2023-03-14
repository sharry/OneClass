import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
import NotificationCard from "../components/NotificationCad";
import RequireAuth from "../components/RequireAuth";

function NotificationPage() {
  return (
    <RequireAuth>
      <div className="h-full">
        <Navbar /> 
        <p className="text-end py-3 pr-6"><a href="" className="underline">Mark all as read </a></p>
        <NotificationCard/>
        <NotificationCard/>
        <NotificationCard/>
        <NotificationCard/>
        <NotificationCard/>
        <NotificationCard/>
        <NotificationCard/>
        <BottomTabs />
      </div>
    </RequireAuth>
  );
}

export default NotificationPage;
