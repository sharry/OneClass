import React, { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { joinClass } from "../api/class";
import OneClasseContext from "../context/OneClassContext";
import RequireAuth from "../components/RequireAuth";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import BottomTabs from "../components/BottomTabs";
// import { useHistory } from "react-dom";

const JoinClass = () => {
  const [code, setCode] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    await joinClass(code);
    navigate("/");
  };

  return (
    <RequireAuth>
      <Navbar />
      <div className="flex flex-col items-center justify-center py-2 sm:px-6 bg-white lg:px-8">
        <div className="w-full max-w-md p-6 my-8 bg-white ">
          <h1 className="text-2xl font-bold">Join Classroom</h1>

          <div className="mt-4">
            <form onSubmit={handleSubmit}>
              <label className="block text-sm">
                <span className="text-gray-700">Join Code</span>
                <input
                  value={code}
                  onChange={(e) => setCode(e.target.value)}
                  className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                  placeholder="Join Code"
                  required
                />
              </label>

              <div className="flex items-end justify-between mt-4">
                <button
                  className="px-4 py-2 text-sm font-medium leading-5 text-white transition-colors duration-150 bg-green-600 border border-transparent rounded-lg active:bg-green-600 hover:bg-green-700 focus:outline-none focus:shadow-outline-blue"
                  type="submit"
                >
                  Join
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

export default JoinClass;
