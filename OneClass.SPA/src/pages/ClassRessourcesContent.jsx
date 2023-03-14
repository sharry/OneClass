import Navbar from "../components/Navbar";
import AttachementCard from "../components/AttachementCard";
import RequireAuth from "../components/RequireAuth";

function ClassResourcesContent() {
  return (
    <RequireAuth>
        <div>
          <Navbar />
          <div className="flex py-6 px-2">
            <img
              alt=""
              src="https://source.unsplash.com/100x100/?portrait"
              className="ml-3 object-cover w-12 h-12 rounded-full shadow dark:bg-gray-500 mr-2"
            />
            <div>
              <h3>
                Teacher Name
                <div className="22">
                  <div className=""></div>
                </div>
              </h3>
              <p>2 minutes ago</p>
            </div>
          </div>
          <hr className="w-11/12 mx-auto" />
          <p className="p-4 text-lg">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Eius distinctio
            nihil, mollitia saepe corrupti necessitatibus. Praesentium vel,
            laboriosam, deleniti assumenda libero officia facilis corrupti vero,
            consequatur consequuntur ab fugit quaerat?
          </p>
          <AttachementCard/>
          <AttachementCard/>
          <AttachementCard/>
        </div>
    </RequireAuth>
  );
}

export default ClassResourcesContent;
