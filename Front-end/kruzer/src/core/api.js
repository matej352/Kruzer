import axios from "axios";

export const api = axios.create({
  baseURL: "https://localhost:7295",
  withCredentials: false,
});
