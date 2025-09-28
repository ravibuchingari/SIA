using SIA.Domain.Entities;

namespace SIA.Client.API.Models
{
    public class SignUpVM
    {
        public UserVM User { get; set; } = null!;
        public OrganizationVM Organization { get; set; } = null!;
    }
}
