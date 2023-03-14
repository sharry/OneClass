function NotificationCard() {
  return (
    <>
      <div className="flex justify-start items-center bg-slate-100 py-2 w-11/12 mx-auto border-b-2">
        <img
          alt=""
          src="https://source.unsplash.com/100x100/?portrait"
          className="ml-3 object-cover w-16 h-16 rounded-full shadow dark:bg-gray-500"
        />
        <div className="flex flex-col ml-4">
          <h1 className="font-medium text-lg">Name</h1>
          <p className="text-gray-500">
            Lorem ipsum dolor sit amet consectetur, adipisi.
          </p>
        </div>
      </div>
    </>
  );
}

export default NotificationCard;
