import React, { useEffect, useState } from "react";
import { getAsync, postAsync, postDataAsync } from "../../../services/apiService";
import { useLocation } from "react-router-dom";
import { toast } from 'react-toastify';
import { CONTROLLER_HOME } from '../../../services/constants';

const CalendarIntegration = () => {
    const [accounts, setAccounts] = useState([]);

    useEffect(() => {
        sessionStorage.setItem("is_calendar_auth", false);
        getAsync("user", `list/user/accounts/true`)
            .then((response) => {
                setAccounts(response.data);
            })
            .catch((error) => {
                console.error(
                    "Error fetching calendar integration URL:",
                    error
                );
            });
    }, []);

     const calendarEvents = async (userAccountId, userAccountGuid) => {
            try {
                const json = {};
                json["userAccountId"] = userAccountId;
                json["userAccountGuId"] = userAccountGuid;
                json["securityKey"] = localStorage.getItem("securityKey");
                json["calendarMonth"] = "10-10-2025";
                await postAsync("calendar", `user/account/events`, json).then((response) => {  
                    console.log("Calendar events fetched:", response.data);
                }
                ).catch((error) => {
                    toast.error(error.response?.data || error.message);
                });
            } catch (error) {
                toast.error(`Fetching calendar events failed: ${error}`);
            }   
        }

    const buttonClick = (userAccountId, userAccountGuid) => {

         try {
                postAsync(CONTROLLER_HOME, "google/client","").then((response) => {
                //handleLogin(response.data.clientId, response.data.redirectUri);

                const scope = 'openid profile email https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events'; // https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events
                const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?` +
                `client_id=${response.data.clientId}` +
                `&redirect_uri=${encodeURIComponent(response.data.redirectUri)}` +
                `&response_type=code` +
                `&scope=${encodeURIComponent(scope)}` +
                `&access_type=offline` +
                `&prompt=consent`;

                sessionStorage.setItem("is_calendar_auth", true);
                sessionStorage.setItem("userAccountId", userAccountId);
                sessionStorage.setItem("userAccountGuId", userAccountGuid);

                window.location.href = authUrl;

            }).catch((error) => {
                toast.error(error.response?.data || error.message);
            });
        } catch (error) {
            toast.error(`Google login failed: ${error}`);
        }

         

        // console.log("userAccountId:", userAccountId);
        // console.log("userAccountGuid:", userAccountGuid);

        // const formData = new FormData();
        // formData.append("userAccountId", userAccountId);
        // formData.append("userAccountGuId", userAccountGuid);
        // formData.append("code", "3213123");
        // formData.append("securityKey", "sdfdsfasd");

        // postDataAsync("calendar", "user/account/auth", formData)
        //     .then((response) => {
        //         console.log("Connection initiated:", response.data);
        //     })
        //     .catch((error) => {
        //         console.error("Error initiating connection:", error);
        //     });

    };

    return (
        <div style={{ textAlign: "center", marginTop: "100px" }}>
            <div>
                <table className="table table-bordered" style={{ marginTop: "20px" }}>
                    <thead>
                        <tr>
                            <th>Provider</th>
                            <th>Email</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                            {accounts.map((account) => (
                                <tr key={account.userAccountId}>
                                    <td>{account.provider}</td>
                                    <td>{account.email}</td>
                                    <td>{account.status}</td>
                                    <td><button className="btn btn-primary" onClick={() => calendarEvents(account.userAccountId, `${account.userAccountGuid}`)}>Connect</button></td>
                                </tr>
                            ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CalendarIntegration;
