import { useIsAuthenticated } from "@azure/msal-react";
import { useNavigate } from "react-router-dom";

function RequireAuth({ children }) {
  const isAuthenticated = useIsAuthenticated();
  const navigate = useNavigate();

  if (!isAuthenticated) {
    navigate("/signin");
  }

  return children;
}

export default RequireAuth;
