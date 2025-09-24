import React from "react";
import { useState } from "react";
import { toast } from "react-toastify";
import { getAsync, signIn } from "../services/apiService";
import { CONTROLLER_HOME } from "../services/constants";
import { useNavigate } from "react-router-dom";
import bannerLogo from '../assets/signin.jpg';
import googleLogo from '../assets/google.svg';
import microsoftLogo from '../assets/microsoft.svg';


const SignIn = () => {

    const socialMediaIcon = {
        padding: '12px',
        border: 'solid 1px #ddd',
        borderRadius: '16px',
        cursor: 'pointer',
        width: '48px',
        height: '48px',
        objectFit: 'contain',
        margin: '4px',
    };


    const navigate = useNavigate();

    // const handleSubmit = (e) => {
    //     const params = new URLSearchParams({
    //         search: "anime",
    //         page: 2,
    //     });
    //     getAsync(CONTROLLER_HOME, "test", params.toString())
    //         .then((response) => {
    //             console.log(response.data);
    //             toast.success("Data fetched successfully!");
    //         })
    //         .catch((error) => {
    //             console.error("Error fetching data:", error);
    //             toast.error("Failed to fetch data.");
    //         });
    // };

    const [formData, setFormData] = useState({
        email: "",
        password: "",
        rememberMe: false,
    });
    const [errors, setErrors] = useState({});
    const [isLoading, setIsLoading] = useState(false);

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value,
        }));

        // Clear error when user starts typing
        if (errors[name]) {
            setErrors((prev) => ({
                ...prev,
                [name]: "",
            }));
        }
    };

    const validateForm = () => {
        const newErrors = {};

        if (!formData.email) {
            newErrors.email = "Email is required";
        } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = "Please enter a valid email";
        }

        if (!formData.password) {
            newErrors.password = "Password is required";
        } else if (formData.password.length < 6) {
            newErrors.password = "Password must be at least 6 characters";
        }

        return newErrors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newErrors = validateForm();
        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        setIsLoading(true);

        // Simulate API call
        setTimeout(() => {
            setIsLoading(false);
            alert(
                `Login successful!\nEmail: ${formData.email}\nRemember me: ${formData.rememberMe}`
            );
        }, 1500);
    };

    const handleSignUp = (e) => {
        navigate("/signup");
    };

    return (
        <div className="container-fluid">
            <div className="row g-0">
                <div className="col-md py-4 ps-4">
                    <div style={{width: '100%', height: '100%'}} className="align-items-center justify-content-center">
                        <img src={bannerLogo} style={{maxWidth: '100%', maxHeight: '100%'}} />
                    </div>
                </div>
                <div className="col-md-auto">
                    <div className="container-400 my-5">
                        <div className="card border-0 p-4 w-100">
                            <div className="card-body">
                                <div className="text-center mb-4">
                                    <h1 className="fw-bold mb-2">Sign in</h1>
                                    <p className="text-muted">
                                        Please sign in to your account
                                    </p>
                                </div>

                                <div onSubmit={handleSubmit}>
                                    <div className="mb-3">
                                        <label
                                            htmlFor="email"
                                            className="form-label fw-semibold"
                                        >
                                            Email Address
                                        </label>
                                        <input
                                            type="email"
                                            className={`form-control form-control-lg ${
                                                errors.email ? "is-invalid" : ""
                                            }`}
                                            id="email"
                                            name="email"
                                            value={formData.email}
                                            onChange={handleInputChange}
                                            placeholder="Enter your email"
                                            disabled={isLoading}
                                        />
                                        {errors.email && (
                                            <div className="invalid-feedback">
                                                {errors.email}
                                            </div>
                                        )}
                                    </div>

                                    <div className="mb-3">
                                        <label
                                            htmlFor="password"
                                            className="form-label fw-semibold"
                                        >
                                            Password
                                        </label>
                                        <input
                                            type="password"
                                            className={`form-control form-control-lg ${
                                                errors.password
                                                    ? "is-invalid"
                                                    : ""
                                            }`}
                                            id="password"
                                            name="password"
                                            value={formData.password}
                                            onChange={handleInputChange}
                                            placeholder="Enter your password"
                                            disabled={isLoading}
                                        />
                                        {errors.password && (
                                            <div className="invalid-feedback">
                                                {errors.password}
                                            </div>
                                        )}
                                    </div>

                                    <div className="text-end mb-3">
                                        <a
                                            href="#"
                                            className="text-decoration-none text-primary fw-normal"
                                        >
                                            Forgot Password?
                                        </a>
                                    </div>

                                    <button
                                        type="submit"
                                        className="btn btn-primary btn-lg w-100 mb-3"
                                        disabled={isLoading}
                                    >
                                        {isLoading ? (
                                            <>
                                                <span
                                                    className="spinner-border spinner-border-sm me-2"
                                                    role="status"
                                                    aria-hidden="true"
                                                ></span>
                                                Signing In...
                                            </>
                                        ) : (
                                            "Sign In"
                                        )}
                                    </button>

                                    <div className="position-relative mb-3">
                                        <hr />
                                        <span className="position-absolute top-50 start-50 translate-middle bg-white text-muted small">
                                            Or continue with
                                        </span>
                                    </div>

                                    <div className="my-4 text-center">
                                       <img src={googleLogo} style={socialMediaIcon}></img>
                                       <img src={microsoftLogo} style={socialMediaIcon}></img>
                                    </div>
                                </div>

                                <div className="text-center">
                                    <span className="text-muted">
                                        Don't have an account?{" "}
                                    </span>
                                    <a
                                        href="#"
                                        className="text-decoration-none fw-normal"
                                        onClick={handleSignUp}
                                    >
                                        Sign up here
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SignIn;
