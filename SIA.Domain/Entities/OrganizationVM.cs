using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime CreatedDate { get; set; }
        public long? ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long? DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public byte SubscriptionId { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsEmailVerified { get; set; }
        public byte OrganizationStatusId { get; set; }
    }
}
