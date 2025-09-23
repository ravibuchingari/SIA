using Microsoft.AspNetCore.Identity;

namespace SIA.Client.API.Models
{
    public class Role : IdentityRole
    {
        public string? RoleDescription { get; set; }
    }
}
