namespace SIA.Domain.Entities
{
    public class OrganizationVM
    {
        public int OrganizationId { get; set; }
        public Guid OrganizationGuid { get; set; }
        public string OrganizationName { get; set; } = null!;
        public int OrganizationSize { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public long? CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public long? DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public byte SubscriptionId { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsEmailVerified { get; set; }
        public byte OrganizationStatusId { get; set; }
        public DateTime EmailVerificationTokenTime { get; set; } = DateTime.Now.AddDays(-1);
    }
}
