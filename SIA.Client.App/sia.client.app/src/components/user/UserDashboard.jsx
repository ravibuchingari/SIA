import { getAsync } from "../../services/apiService";
import { toast } from 'react-toastify';

export default function UserDashboard() {

    const test = () => { 

        getAsync("Dashboard", "test").then((response) => {
            alert("Success: " + JSON.stringify(response.data));
        }).catch((error) => {
            console.log(error);
             toast.error("There was an error! " + (error.response?.data || error.message));
        });
    }

    const logout = () => { 
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        //window.location.href = '/signin';
    }

    return (
        <div style={{padding: '20px', marginTop: '60px'}}>
            <h2>User Dashboard</h2>
            <p>Welcome to the user dashboard!</p>

            <div>
                <button className="btn btn-primary" onClick={test}>Test Token</button>
            </div>

            <div className="mt-5">
                <button className="btn btn-danger" onClick={logout}>logout</button>
            </div>
        </div>
    )
}