import React from 'react';
import { toast } from 'react-toastify';
import './css/SignIn.css';

const SignIn = () => { 

    const handleSubmit = (e) => { 
        toast.success("SignIn Clicked");
    }

    return ( 
        <div className="bgColor" onClick={handleSubmit}>SignIn</div>
    )
}

export default SignIn;