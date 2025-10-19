import React, {useState, useEffect} from 'react';
import { GoogleLogin, useGoogleLogin } from '@react-oauth/google';
import googleLogo from "../../../assets/google.svg";
import { toast } from 'react-toastify';
import { postAsync } from '../../../services/apiService';
import { CONTROLLER_HOME } from '../../../services/constants';
import { useNavigate } from 'react-router-dom';



const GoogleAuthentication = () => {
  const navigate = useNavigate();
  //const [clientId, setClientId] = useState(null);
  //const [clientId, setClientId] = useState("");
  const socialMediaIcon = {
        padding: "8px",
        border: "solid 1px #ddd",
        borderRadius: "10px",
        cursor: "pointer",
        width: "64px",
        height: "48px",
        objectFit: "contain",
        margin: "4px",
    };

    const executeLogin = async () => {
      try {
            await postAsync(CONTROLLER_HOME, "google/client","").then((response) => {
              //setClientId(response.data.message);
              //console.log("Fetched Google Client ID:", response.data.message);
              handleLogin(response.data.clientId, response.data.redirectUri);
            }).catch((error) => {
              toast.error(error.response?.data || error.message);
            });
        } catch (error) {
            toast.error(`Google login failed: ${error}`);
        }
    }

  const handleLogin = (clientId, redirectUri) => {
    if(!clientId || clientId.trim() === ""){
        toast.error("Google Client ID is not set.");
        return;
    }

    const scope = 'openid profile email https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events'; // https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events
    const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?` +
      `client_id=${clientId}` +
      `&redirect_uri=${encodeURIComponent(redirectUri)}` +
      `&response_type=code` +
      `&scope=${encodeURIComponent(scope)}` +
      `&access_type=offline` +
      `&prompt=consent`;
sessionStorage.setItem("is_calendar_auth", false)
    window.location.href = authUrl;
  };

  return (
      <div style={{userSelect: 'none', textAlign: 'center'}} onClick={() => executeLogin()}>
          <div>
              <img
                  src={googleLogo}
                  style={socialMediaIcon}
              ></img>
          </div>
          <div>Google</div>
      </div>
  );
};

export default GoogleAuthentication;