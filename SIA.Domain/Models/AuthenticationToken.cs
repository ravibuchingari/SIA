using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Models
{
    public class AuthenticationToken
    {
        public string AccessToken { get; set; } = null!;
        public string? RefreshToken { get; set; } // or refresh kek
    }
}
