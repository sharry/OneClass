import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../config/msalConfig";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export const SignInButton = ({ loginType }) => {
  const { instance } = useMsal();
  const navigate = useNavigate();

  const handleLogin = (loginType) => {
    if (loginType === "popup") {
      instance
        .loginPopup(loginRequest)
        .then(() => {
          axios
            .get("/users/me")
            .then(() => {
              navigate("/");
            })
            .catch(() => {
              console.log("Error while getting user data");
            });
        })
        .catch((e) => {
          console.log(e);
        });
    } else if (loginType === "redirect") {
      instance
        .loginRedirect(loginRequest)
        .then((res) => {
          navigate("/");
        })
        .catch((e) => {
          console.log(e);
        });
    }
  };
  return (
    <button
      type="button"
      className="bg-green-700 text-white px-10 py-2"
      onClick={() => handleLogin(loginType)}
    >
      Sign in
    </button>
  );
};
