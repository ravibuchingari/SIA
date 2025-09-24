import { useState, useEffect } from "react";
import { ArrowRight, ArrowBigRight } from "lucide-react";

const SignUp = () => {
    const initialValues = { firstName: "", lastName: "", displayName: "", email: "", password: "", confirmPassword: "" };
    const [formValues, setFormValues] = useState(initialValues);
    const [formErrors, setFormErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);

    const handleChange = (e) => { 
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
    }

    const handleSubmit = (e) => { 
        e.preventDefault();
        setFormErrors(validate(formValues));
        setIsSubmit(true);
    }

    useEffect(() => { 
        if (Object.keys(formErrors).length === 0 && isSubmit) { 
            console.log(formValues);
        }
    }, [formErrors]);


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
    } 
       
    return (
        <div className="container p-5">
            <div className="card shadow-lg border-0 rounded-4 p-4 w-100">
                <form onSubmit={handleSubmit}>
                    <div>
                        <div className="d-flex mb-3 page-title">
                            <ArrowBigRight size={22} style={{color: "inherit"}} className="mt-1"/>
                            <h4 className="ms-1">Create your account</h4>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                First Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="First Name"
                                maxLength="50"
                                value={formValues.firstName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.firstName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Last Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Last Name"
                                maxLength="50"
                                value={formValues.lastName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.lastName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Display Name
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Display Name"
                                maxLength="100"
                                value={formValues.displayName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.displayName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Email
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Email"
                                maxLength="150"
                                value={formValues.email}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.email}</div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                First Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="First Name"
                                maxLength="50"
                                value={formValues.firstName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.firstName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Last Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Last Name"
                                maxLength="50"
                                value={formValues.lastName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.lastName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Display Name
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Display Name"
                                maxLength="100"
                                value={formValues.displayName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.displayName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Email
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Email"
                                maxLength="150"
                                value={formValues.email}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.email}</div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                First Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="First Name"
                                maxLength="50"
                                value={formValues.firstName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.firstName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Last Name
                            </label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Last Name"
                                maxLength="50"
                                value={formValues.lastName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.lastName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Display Name
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Display Name"
                                maxLength="100"
                                value={formValues.displayName}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.displayName}</div>
                        </div>
                        <div className="col-md mb-3">
                            <label htmlFor="email" className="form-label fw-semibold">
                                Email
                            </label>
                            <input
                                type="email"
                                className="form-control"
                                placeholder="Email"
                                maxLength="150"
                                value={formValues.email}
                                onChange={handleChange}
                            />
                            <div className="error">{formErrors.email}</div>
                        </div>
                    </div>

                    <div className="text-end mt-3">
                        <button className="btn btn-primary">Create Account</button>
                    </div>               
                </form>
            </div>
        </div>
    );
};

export default SignUp;
