namespace SIA.Domain.Models
{

    public enum RowStatus
    {
        Active, Deactivated, Deleted
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

    public enum SubscriptionPlans
    {
        Free = 1,
        Pro = 2,
        Enterprise = 3,
    }

    public enum EmailCode
    {
        SendMailOnEmailVerification = 1,
    }

    public enum CalendarStatus
    {
        Pending, Active, Deactivated, Removed
    }

    public enum SessionStatus
    {
        Active, Inactive, Expired, Terminated
    }

    public enum DeviceType
    {
        Desktop, Mobile, Tablet, Web, Other
    }

    public enum ParticipantStatus
    {
        NeedsAction, Declined, Tentative, Accepted
    }

    public enum MeetingType
    { 
        Virtual, InPerson, Hybrid
    }

    public enum SIANoteTakingLevel
    {
        None, Manual, AutoTrash
    }

    public enum SIANoteContentType
    {
        All, Redacted
    }

    public enum MeetingDriver
    {
        SIA, USER
    }

    public enum BillingCycle
    {
        Monthly, Yearly
    }

    public enum RenewalType
    {
        AutoRenew, Manual
    }

    public enum InvoiceStatus
    {
        Paid, Pending, Overdue, Void, Draft
    }

    public enum BillingStatus
    { 
        Paid, Pending, Failed, Refunded, Disputed
    }

    public enum SupportLevel
    {
        Basic, Priority, Support24x7
    }

    public enum DiscountType
    {
        Percentage, FixedAmount
    }

}
