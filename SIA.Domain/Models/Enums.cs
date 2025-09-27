namespace SIA.Domain.Models
{

    public enum AccountStatus
    {
        EmailValidation = 1,
        Active = 2,
        Suspended = 3,
        Deleted = 4
    }

    public enum OrgStatus
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

    public enum Subscriptions
    {
        FreePlan = 1,
        PaidPlan = 2,
        BusinessPlan = 3,
    }

    public enum EmailCode
    {
        SendMailOnEmailVerification = 1,
    }
}
