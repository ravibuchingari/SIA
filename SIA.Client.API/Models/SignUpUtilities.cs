using SIA.Domain.Entities;

namespace SIA.Client.API.Models
{
    public class SignUpUtilities
    {
        public IEnumerable<LanguageVM> Languages { get; set; } = [];
        public IEnumerable<TimeZoneVM> TimeZones { get; set; } = [];
    }
}
