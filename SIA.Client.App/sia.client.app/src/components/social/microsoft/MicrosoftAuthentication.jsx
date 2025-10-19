import React, {useState, useEffect} from 'react';
import microsoftLogo from "../../../assets/microsoft.svg";
import { useMsal } from "@azure/msal-react";
import { toast } from 'react-toastify';
import { postAsync } from '../../../services/apiService';
import { CONTROLLER_HOME } from '../../../services/constants';
//import { useNavigate } from 'react-router-dom';
const MicrosoftAuthentication = () => {
 const [clientId, setClientId] = useState(null);
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
              await postAsync(CONTROLLER_HOME, "microsoft/client","").then((response) => {
                handleLogin(response.data.clientId, response.data.tenantId, response.data.redirectUri);
              }).catch((error) => {
                toast.error(error.response?.data || error.message);
              });
          } catch (error) {
              toast.error(`Microsoft login failed: ${error}`);
          }
      }

 // Generate a random code_verifier
function generateCodeVerifier(length = 64) {
  const charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
  let verifier = "";
  const randomValues = new Uint32Array(length);
  crypto.getRandomValues(randomValues);
  for (let i = 0; i < length; i++) {
    verifier += charset[randomValues[i] % charset.length];
  }
  return verifier;
}

// Base64URL encode
function base64UrlEncode(buffer) {
  return btoa(String.fromCharCode.apply(null, new Uint8Array(buffer)))
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=+$/, "");
}

// Generate code_challenge from verifier
async function generateCodeChallenge(verifier) {
  const encoder = new TextEncoder();
  const data = encoder.encode(verifier);
  const digest = await crypto.subtle.digest("SHA-256", data);
  return base64UrlEncode(digest);
}




  const handleLogin = (clientId, tenantId, redirectUri) => {

    const verifier = generateCodeVerifier();
    generateCodeChallenge(verifier).then(challenge => {

      const codeVerifier = generateCodeVerifier(); // ~86 characters
      const codeChallenge = generateCodeChallenge(codeVerifier);

      const scope = 'openid profile email offline_access user.read';
      const authUrl = `https://login.microsoftonline.com/${tenantId}/oauth2/v2.0/authorize?` +
        `client_id=${clientId}` +
        `&response_type=code` +
        `&redirect_uri=${encodeURIComponent(redirectUri)}` +
        `&response_mode=query` +
        `&scope=${encodeURIComponent(scope)}` +
        `&code_challenge=${challenge}` +
        `&code_challenge_method=S256` +
        `&prompt=consent`;

        sessionStorage.setItem("codeVerifier", verifier);
        window.location.href = authUrl;
     
    });

    
  };

  return( <div style={{userSelect: 'none', textAlign: 'center'}} onClick={() => executeLogin()}>
            <div>
                <img
                    src={microsoftLogo}
                    style={socialMediaIcon}
                ></img>
            </div>
            <div>Microsoft</div>
        </div>

  )
};

export default MicrosoftAuthentication;
