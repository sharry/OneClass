import React, { useState } from "react";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
import NotificationCard from "../components/NotificationCad";
import RequireAuth from "../components/RequireAuth";
 
function NotificationPage() {
  const [notifications, setNotifications] = useState([1]);

  return (
    <RequireAuth>
      <div className="h-full">
        <Navbar />
        {notifications.length > 0 ? (
          <div className="">
            <p className="text-end py-3 pr-6">
              <a href="" className="underline">
                Mark all as read{" "}
              </a>
            </p>
            <NotificationCard />
            <NotificationCard />
            <NotificationCard />
            <NotificationCard />
            <NotificationCard />
            <NotificationCard />
            <NotificationCard />
          </div>
        ) : (
          <div className="flex flex-col justify-center items-center">
            <img src="../assets/notificationNotFound.png" alt="" />
            <h1 className="text-3xl font-semibold text-gray-500">
              No Notification Here
            </h1>
            <button className="py-2 px-4 bg-green-600 text-white rounded-lg mt-4">
              Return to classes page
            </button>
          </div>
        )}

        <BottomTabs />
      </div>
    </RequireAuth>
  );
}

export default NotificationPage;
