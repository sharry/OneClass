import React, { useState } from "react";
import RequireAuth from "../components/RequireAuth";
import { createResource } from "../api/class";
import { useParams } from "react-router-dom";

const AddResource = ({loadContent}) => {
  const { id } = useParams();
  const [content, setContent] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    createResource(id, content);
    setContent("");
  };

  return (
    <RequireAuth>
      <div className="px-4 mb-2">
        <form onSubmit={handleSubmit}>
          <label className="block text-sm">
            <span className="text-gray-700">New Resource</span>
            <textarea
              value={content}
              onChange={(e) => setContent(e.target.value)}
              className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
              placeholder="New Resource"
              required
            ></textarea>
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
      <hr className="mb-5" />
    </RequireAuth>
  );
};

export default AddResource;
