using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Entities
{
    public class SignInSuccessResponse
    {
        public int OrganizationId { get; set; }
        public string OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public long UserId { get; set; }
        public string UserGuid { get; set; }
        public string DisplayName { get; set; }
        public string SecretKey { get; set; }
        public string SecurityKey { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RoleName { get; set; }
    }
}
