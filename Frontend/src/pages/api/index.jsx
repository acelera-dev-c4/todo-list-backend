import axios from "axios";

export const baseURL = "https://aceleradev.sharebook.com.br"

const api = async (method, rota, data) => {
  const headers = {
    // withCredentials: true,
    "Content-Type": "application/json",
    Accept: "application/json",
  };

  try {
    let response;
    switch (method) {
      case "get":
        response = await axios.get(`${baseURL}${rota}`, headers);
        break;
      case "post":
        response = await axios.post(`${baseURL}${rota}`, data, headers);
        break;
      case "put":
        response = await axios.put(`${baseURL}${rota}`, data, headers);
        break;
      case "delete":
        response = await axios.delete(`${baseURL}${rota}`, headers);
        break;
      default:
        throw new Error("Invalid method");
    }
    return response;
  } catch (error) {
    console.error("Error request: ", error);
    throw error;
  }
};

export default api;
