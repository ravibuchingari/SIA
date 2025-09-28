import { useState, useEffect } from "react";
import { getAsync, postAsync } from "../services/apiService";
import bannerLogo from "../assets/signin.jpg";
import { toast } from "react-toastify";
import { CONTROLLER_HOME } from "../services/constants";
// import { ArrowRight, ArrowBigRight } from "lucide-react";

const SignUp = () => {
    const initialValues = {
        firstName: "",
        lastName: "",
        displayName: "",
        email: "",
        password: "",
        confirmPassword: "",
    };
    const [formValues, setFormValues] = useState(initialValues);
    const [formErrors, setFormErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);
    const [languages, setLanguages] = useState([]);
    const [timeZones, setTimeZones] = useState([]);

    useEffect(() => {
        document.title = `Sign Up - ${import.meta.env.VITE_APP_NAME}`;
        getAsync(CONTROLLER_HOME, "signup/utilities").then((response) => {
            if(response.data){
                setLanguages(response.data.languages);
                setTimeZones(response.data.timeZones);
            }
            else {
                toast.error("Unable to fetch data!");
            }
        }
        ).catch((error) => {
            toast.error("There was an error! " + (error.response?.data?.message || error.message))
        });
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        setFormErrors(validate(formValues));
        setIsSubmit(true);
        //console.log(JSON.stringify(formValues))
        postAsync(CONTROLLER_HOME, "signup/account", formValues).then((response) => {
            toast.success("success");
        }).catch((error) => {
            console.log(error);
            toast.error(`${error.response?.data?.code || error.code}: ` + (error.response?.data?.message || error.message))
        });
    };

    // useEffect(() => {
    //     if (Object.keys(formErrors).length === 0 && isSubmit) {
    //         toast.error(JSON.stringify(formErrors));
    //     }
    // }, [formErrors]);

    const validate = (values) => {
        const errors = {};
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
        if (!values.firstName) {
            errors.firstName = "First Name is required!";
        }
        if (!values.lastName) {
            errors.lastName = "Last Name is required!";
        }
        if (!values.displayName) {
            errors.displayName = "Display Name is required!";
        }
        if (!values.email) {
            errors.email = "Email is required!";
        } else if (!regex.test(values.email)) {
            errors.email = "This is not a valid email format!";
        }
        // if (!values.password) {
        //     errors.password = "Password is required!";
        // } else if (values.password.length < 6) {
        //     errors.password = "Password must be more than 6 characters";
        // }
        // if (!values.confirmPassword) {
        //     errors.confirmPassword = "Confirm Password is required!";
        // }   else if (values.confirmPassword !== values.password) {
        //     errors.confirmPassword = "Passwords do not match!";
        // }
        return errors;
    };

    return (
        <div className="container-fluid container-fluid-custom">
            <div className="row g-0" style={{ height: "calc(100vh - 70px)" }}>
                <div className="col-md">
                    <div
                        style={{ width: "100%", height: "100%" }}
                        className="center-block">
                        <img src={bannerLogo} style={{ display: 'block', width: "auto", height: "calc(100vh - 170px)" }} />
                    </div>
                </div>
                <div className="col-md-auto" style={{backgroundColor: "#f8f9fa" }}>
                    <div className="container-500">
                        <div className="card border-0 p-4 w-100" style={{ backgroundColor: "inherit" }}>
                            <form onSubmit={handleSubmit}>
                                <div className="text-center mb-5">
                                    <h2>Create your account</h2>
                                </div>

                                <div className="mb-1">
                                    <label
                                        htmlFor="firstName"
                                        className="form-label fw-semibold"
                                    >
                                        First Name
                                    </label>
                                    <input
                                        type="text"
                                        name="firstName"
                                        className="form-control"
                                        maxLength="50"
                                        value={formValues.firstName}
                                        onChange={handleChange}
                                        required
                                    />
                                    <div className="error">
                                        {formErrors.firstName}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <label
                                        htmlFor="lastName"
                                        className="form-label fw-semibold"
                                    >
                                        Last Name
                                    </label>
                                    <input
                                        type="text"
                                        name="lastName"
                                        className="form-control"
                                        maxLength="50"
                                        value={formValues.lastName}
                                        onChange={handleChange}
                                        required
                                    />
                                    <div className="error">
                                        {formErrors.lastName}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <label
                                        htmlFor="displayName"
                                        className="form-label fw-semibold"
                                    >
                                        Display Name
                                    </label>
                                    <input
                                        type="text"
                                        name="displayName"
                                        className="form-control"
                                        maxLength="100"
                                        value={formValues.displayName}
                                        onChange={handleChange}
                                        required
                                    />
                                    <div className="error">
                                        {formErrors.displayName}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <label
                                        htmlFor="email"
                                        className="form-label fw-semibold"
                                    >
                                        Email
                                    </label>
                                    <input
                                        type="email"
                                        name="email"
                                        className="form-control"
                                        maxLength="150"
                                        value={formValues.email}
                                        onChange={handleChange}
                                        required
                                    />
                                    <div className="error">
                                        {formErrors.email}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <label
                                        htmlFor="countryCode"
                                        className="form-label fw-semibold"
                                    >
                                        Country Code
                                    </label>
                                    <input
                                        type="text"
                                        name="countryCode"
                                        className="form-control"
                                        maxLength="5"
                                        value={formValues.countryCode}
                                        onChange={handleChange}
                                        required
                                    />
                                    <div className="error">
                                        {formErrors.countryCode}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <label
                                        htmlFor="phoneNumber"
                                        className="form-label fw-semibold"
                                    >
                                        Phone Number
                                    </label>
                                    <input
                                        type="text"
                                        name="phoneNumber"
                                        className="form-control"
                                        maxLength="5"
                                        value={formValues.phoneNumber}
                                        onChange={handleChange}
                                    />
                                    <div className="error">
                                        {formErrors.phoneNumber}
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <div className="row g-1">
                                        <div className="col">
                                            <label
                                                htmlFor="language"
                                                className="form-label fw-semibold"
                                            >
                                                Language
                                            </label>
                                            <select
                                                className="form-select"
                                                name="language"
                                                value={formValues.timeFormat}
                                                onChange={handleChange}
                                            >
                                                {
                                                    languages.map((lang) => (
                                                        <option key={lang.languageCode} value={lang.languageCode}>{lang.languageName}</option>
                                                    ))
                                                }
                                            </select>
                                        </div>
                                        <div className="col">
                                            <label
                                                htmlFor="timeZone"
                                                className="form-label fw-semibold"
                                            >
                                                Time Zone
                                            </label>
                                             <select
                                                className="form-select"
                                                name="language"
                                                value={formValues.timeFormat}
                                                onChange={handleChange}
                                            >
                                               {
                                                    timeZones.map((tz) => (
                                                        <option key={tz.timeZoneName} value={tz.timeZoneName}>{tz.commonRegions}</option>
                                                    ))
                                               }
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div className="mb-1">
                                    <div className="row g-1">
                                        <div className="col">
                                            <label
                                                htmlFor="dateFormat"
                                                className="form-label fw-semibold"
                                            >
                                                Date Format
                                            </label>
                                            <select
                                                className="form-select"
                                                name="dateFormat"
                                                value={formValues.dateFormat}
                                                onChange={handleChange}
                                            >
                                                <option value="YYYY-MM-DD">
                                                    YYYY-MM-DD
                                                </option>
                                                <option value="24">
                                                    DD-MM-YYYY
                                                </option>
                                                <option value="24">
                                                    DD-MMM-YYYY
                                                </option>
                                            </select>
                                        </div>
                                        <div className="col">
                                            <label
                                                htmlFor="timeFormat"
                                                className="form-label fw-semibold"
                                            >
                                                Time Format
                                            </label>
                                            <select
                                                className="form-select"
                                                name="timeFormat"
                                                value={formValues.timeFormat}
                                                onChange={handleChange}
                                            >
                                                <option value="12H">
                                                    12 Hour
                                                </option>
                                                <option value="24H">
                                                    24 Hour
                                                </option>
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                <div className="text-center mt-5">
                                    <button className="btn btn-primary w-100">
                                        Create Account
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SignUp;
