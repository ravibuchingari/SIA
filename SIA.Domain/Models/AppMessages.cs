namespace SIA.Domain.Models
{
    public static class AppMessages
    {
        public const string COOKIE_REFRESH_TOKEN = "refreshToken";
        public const string EMAIL_VERIFICATION_ERROR = "EMAIL_VERIFICATION_ERROR";
        public const string SUCCESS = "SUCCESS";

        public const string RequiredField = "This field is required.";
        public const string InvalidEmail = "Please enter a valid email address.";
        public const string UnauthorizedAccess = "You do not have permission to perform this action.";
        public const string DuplicateEmail = "Email already exists";
        public const string DuplicateUsername = "Username already exists";
        public const string AccountSuccess = "Account created successfully.";
        public const string ProviderDeactivated = "The provider authentication is temporarily deactivated.";
        public const string GoogleAuthenticationFailed = "Google authentiation failed.";
        public const string GoogleUserVerificationFailed = "Google sign-in was successful, but we couldn’t verify your account. Please contact support or try again";
        public const string DuplicateOrganizationEmail = "This organization email address is already assigned to another account";
        public const string AlreadyConvertedToBusiness = "Sorry, the account has already been migrated to a business account";
        public const string ConvertedToBusinessSuccess = "Your account has been migrated to a business account successfully";
        public const string MailServerNotConfigured = "SMTP server is not configured.";
        public const string MailMessageNotConfigured = "Mail message is not configured.";
        public const string AuthenticationFailed = "Authentication failed.";
        public const string UserSuspended = "Your account has been suspended. Please contact the administrator";
        public const string AccountSuspended = "Your account has been suspended. Please contact the administrator";
        public const string SocialAuthFailed = "Authentication with your provider failed. Please contact the service provider for more information";
    }
}
