import microsoftLogo from "../../../assets/microsoft.svg";
import { useMsal } from "@azure/msal-react";

const MicrosoftAuthentication = () => {

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


  // const handleLogin = async () => {

  //   // const response = await msalInstance.loginPopup({
  //   //   scopes: ["openid", "profile", "email"]
  //   // });
  //   // console.log("Microsoft Login Response:", response);
    
  //   // const token = response.accessToken;
   
  // };

  const { instance } = useMsal();
  const handleLogin = () => {
    instance.loginRedirect({ scopes: ["User.Read"] });
  };

  return( <div style={{userSelect: 'none', textAlign: 'center'}} onClick={() => handleLogin()}>
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
