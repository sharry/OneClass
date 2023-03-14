function TodoCard() {
  return (
    <>
      <div className="flex justify-around items-center py-2 border-b-2 mx-2">
        <img
          alt=""
          src="https://source.unsplash.com/100x100/?portrait"
          className="ml-3 object-cover w-16 h-16 rounded-full shadow dark:bg-gray-500"
        />
        <div className="flex flex-col ml-4">
          <h1 className="font-medium text-lg">TP Linux bootable sur windows ...</h1>
          <p className="text-gray-500">
          Classroom name
          </p>
        </div>
        <div className="flex flex-col text-red-600 font-medium">
            <p>Yesterday</p>
            <p>23:00</p>
        </div>
      </div>
    </>
  );
}
export default TodoCard;
