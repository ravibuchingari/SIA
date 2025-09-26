namespace SIA.Domain.Entities
{
    public class AuthConfigVM
    {
        public string AuthProvider { get; set; } = null!;
        public string? ClientId { get; set; }
        public string? SecretKey { get; set; }
        public string? Authority { get; set; }
        public string? TenantId { get; set; }
        public string? RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public string? UserInfoApi { get; set; }
    }
}
