import { useMsal } from "@azure/msal-react";
import { useEffect } from "react";

const AuthenticationSuccess = () => {

    // const { instance } = useMsal();

    // const request = {
    //     scopes: ["user.read"],
    // };

    // useEffect(() => {
    //     instance.acquireTokenSilent(request).then((response) => {
    //         fetch("https://yourapi.com/api/sample", {
    //             headers: {
    //                 Authorization: `Bearer ${response.accessToken}`,
    //             },
    //         });
    //     });
    // }, []);

    return (
        <div style={{marginTop: '100px'}}>
            <div>Authentication successfuly completed</div>
            <button>TEst</button>
            <button>TEst</button>
            <button>TEst</button>
            <button>TEst</button>
            <button>TEst</button>
            <button>TEst</button>
        </div>
    );
};

export default AuthenticationSuccess;
