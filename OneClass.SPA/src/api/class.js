import axios from "../config/axiosConfig";

// api for class

// create a new class
// POST /api/classrooms
// body: {title: string, description: string, subject: string ,description: string, image: string}
// return: {statusCode: number}
export const createClass = async (title, description, subject, image) => {
  const response = await fetch("/api/classrooms", {
    method: "POST",
    body: JSON.stringify({ title, description, subject, image }),
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });
  return response;
};

export const getClasses = async () => {
  const response = await axios.get(`/classrooms`);
  return response.data;
};

export const getClassResources = async ({ queryKey }) => {
  const id = queryKey[1];
  const response = await axios.get(`/classrooms/${id}/resources`);
  return response.data;
};

export const getClassMembers = async ({ queryKey }) => {
  const id = queryKey[1];
  const response = await axios.get(`/classrooms/${id}/members`);
  return response.data;
}
