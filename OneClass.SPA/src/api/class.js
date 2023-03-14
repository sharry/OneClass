import axios from "../config/axiosConfig";

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

export const createClass = async (title, description, image) => {
  const response = await axios.post(`/classrooms`, { title, description, image });
  return response.data;
}
