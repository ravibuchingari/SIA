using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Entities
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityKey { get; set; }
        public string SecretKey { get; set; }
    }
}
