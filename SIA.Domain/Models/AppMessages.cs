namespace SIA.Domain.Models
{
    public static class AppMessages
    {
        public const string RequiredField = "This field is required.";
        public const string InvalidEmail = "Please enter a valid email address.";
        public const string UnauthorizedAccess = "You do not have permission to perform this action.";
        public const string DuplicateEmail = "Email already exists";
        public const string AccountSuccess = "Account created successfully.";
        public const string ProviderDeactivated = "The provider authentication is temporarily deactivated.";
        public const string GoogleAuthenticationFailed = "Google authentiation failed.";
        public const string GoogleUserVerificationFailed = "Google sign-in was successful, but we couldn’t verify your account. Please contact support or try again";
    }
}
