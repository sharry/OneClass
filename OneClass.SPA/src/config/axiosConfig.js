import { PublicClientApplication } from "@azure/msal-browser";
import axios from "axios";
import { msalConfig, loginRequest } from "./msalConfig";

const msalInstance = new PublicClientApplication(msalConfig);

axios.defaults.baseURL = "http://localhost:5000/api";
axios.defaults.withCredentials = false;

axios.interceptors.request.use(
  async (config) => {
    if (config.headers) {
      config.headers["Content-Type"] = "application/json";
    }

    const account = msalInstance.getAllAccounts()[0];
    if (account) {
      const accessTokenResponse = await msalInstance.acquireTokenSilent({
        scopes: loginRequest.scopes,
        account,
      });

      if (accessTokenResponse) {
        const accessToken = accessTokenResponse.accessToken;

        if (config.headers && accessToken) {
          config.headers["Authorization"] = "Bearer " + accessToken;
        }
      }
    }
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);

export default axios;
