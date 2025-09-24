import React from 'react';
import { toast } from 'react-toastify';
import { getAsync, signIn} from '../services/apiService';
import { CONTROLLER_HOME } from '../services/constants';
import './css/SignIn.css';

const SignIn = () => { 

    const handleSubmit = (e) => { 
        getAsync(CONTROLLER_HOME, 'test')
            .then(response => {
                console.log(response.data);
                toast.success("Data fetched successfully!");
            })
            .catch(error => {
                console.error("Error fetching data:", error);
                toast.error("Failed to fetch data.");
            });
    }

    return ( 
        <div className="bgColor" onClick={handleSubmit}>SignIn</div>
    )
}

export default SignIn;