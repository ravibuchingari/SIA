import React from 'react';
import { GoogleLogin, useGoogleLogin } from '@react-oauth/google';
import googleLogo from "../../../assets/google.svg";
import { toast } from 'react-toastify';
import { postAsync } from '../../../services/apiService';
import { CONTROLLER_HOME } from '../../../services/constants';

const GoogleAuthentication = () => {

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

  const handleLogin = useGoogleLogin({
    scope: 'openid profile email',
    onSuccess: async codeResponse => {
      try {
        console.log(JSON.stringify(codeResponse))
        await postAsync(CONTROLLER_HOME, "signin/google/validation", JSON.stringify(codeResponse)).then((response) => {
          console.log('User info from backend:', response); 
        }).catch((error) => {
            console.log(error);
            toast.error("There was an error! " + (error.response?.data?.message || error.message))
        });
      } catch (error) {
        toast.error(`Error sending code to backend: ${error}`);
      }
    },
    onError: error => {
      toast.error(`Google login failed: ${error}`);
    },
  });

  return (
      <div style={{userSelect: 'none', textAlign: 'center'}} onClick={() => handleLogin()}>
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