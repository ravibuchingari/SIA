namespace SIA.Domain.Models
{
    public enum UserStatus
    {
        EmailValidation = 1,
        Active = 2,
        Suspended = 3,
        Deleted = 4
    }

    public enum Providers
    {
        Google, Microsoft
    }
}
