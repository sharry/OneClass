import axios from "../config/axiosConfig";

export const getClasses = async () => {
  const response = await axios.get(`/classrooms`);
  return response.data;
};

export const getClassResources = async ({ queryKey }) => {
  const id = queryKey[1];
  const response = await axios.get(`/classrooms/${id}/resources`);

  if (!response.ok) {
    throw new Error(`details/${id} fetch not ok`);
  }
  return response.data;
};

export const getClassMembers = async ({ queryKey }) => {
  const id = queryKey[1];
  const response = await axios.get(`/classrooms/${id}/members`);
  return response.data;
};

export const createClass = async (title, description, image) => {
  const response = await axios.post(`/classrooms`, {
    title,
    description,
    image,
  });
  return response.data;
};

export const joinClass = async (code) => {
  const response = await axios.post(`/classrooms/join`, {
    code,
  });
  return response.data;
};
