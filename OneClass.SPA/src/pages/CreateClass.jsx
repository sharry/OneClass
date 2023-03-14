// this page is for teacher to create a class, it has an image in the top, with a botton on top of it, placed in the bottom right corner, then a classroom name input, a class code input, a subject a discription ,and a create button

import React, { useContext, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createClass } from "../api/class";
import OneClasseContext from "../context/OneClassContext";
import RequireAuth from "../components/RequireAuth";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
// import { useHistory } from "react-dom";

const CreateClass = () => {
  const [image, setImage] = useState("image-01.svg");
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createClass(name, description, image);
    navigate("/");
  };

  return (
    <RequireAuth>
      <Navbar />
      <div className="flex flex-col items-center justify-center py-2 sm:px-6 bg-white lg:px-8">
        <div className="w-full max-w-md p-6 my-8 bg-white ">
          <h1 className="text-2xl font-bold">Create Classroom</h1>

          <div className="mt-4">
            <form onSubmit={handleSubmit}>
              {/* <div className="relative w-full h-64 mb-6 rounded-md overflow-hidden">
                                <img
                                    className="absolute inset-0 w-full h-full object-cover"
                                    src="https://placehold.jp/1280x360.png"
                                    alt="avatar"
                                />
                                <label className="absolute bottom-2 right-2 block px-2 overflow bg-black opacity-50   shadow cursor-pointer text-white">
                                    Choose Image
                                    <input type="file" className="absolute inset-0 w-full h-full opacity-0" />
                                </label>
                            </div> */}
              <div
                style={{
                  backgroundImage: `url('../assets/classrooms/${image}')`,
                }}
                className={` bg-no-repeat bg-cover h-32 mb-4 rounded-2xl py-4 px-8 text-white`}
              ></div>
              <label className="block text-sm">
                <span className="text-gray-700">Class Image</span>
                <select
                  className="block w-full px-3 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                  value={image}
                  onChange={(e) => setImage(e.target.value)}
                >
                  <option value="image-01.svg">Image 1</option>
                  <option value="image-02.svg">Image 2</option>
                  <option value="image-03.svg">Image 3</option>
                  <option value="image-04.svg">Image 4</option>
                </select>
              </label>

              <label className="block text-sm">
                <span className="text-gray-700">Class Name</span>
                <input
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                  placeholder="Class Name"
                  required
                />
              </label>

              <label className="block mt-4 text-sm">
                <span className="text-gray-700">Description</span>
                <textarea
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                  placeholder="Description"
                  required
                />
              </label>

              <div className="flex items-end justify-between mt-4">
                <button
                  className="px-4 py-2 text-sm font-medium leading-5 text-white transition-colors duration-150 bg-green-600 border border-transparent rounded-lg active:bg-green-600 hover:bg-green-700 focus:outline-none focus:shadow-outline-blue"
                  type="submit"
                >
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <BottomTabs />
    </RequireAuth>
  );
};

export default CreateClass;
