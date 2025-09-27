using SIA.Domain.Entities;

namespace SIA.Client.API.Models
{
    public class SignUpVM
    {
        public UserVM UserVM { get; set; } = null!;
        public OrganizationVM OrganizationVM { get; set; } = null!;
    }
}
