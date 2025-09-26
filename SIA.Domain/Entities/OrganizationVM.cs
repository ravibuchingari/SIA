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
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = null!;
        public int OrganizationSize { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
        public byte SubscriptionId { get; set; }
    }
}
