namespace SIA.Domain.Entities
{
    public class AuthConfigVM
    {
        public string AuthProvider { get; set; } = null!;
        public string? ClientId { get; set; }
        public string? ClientSecretKey { get; set; }
        public string? Authority { get; set; }
        public string? TenantId { get; set; }
        public string? RedirectUrl { get; set; }
        public string? UserinfoApi { get; set; }
    }
}
