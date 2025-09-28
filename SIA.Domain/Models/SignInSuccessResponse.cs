namespace SIA.Domain.Models
{
    public class SignInSuccessResponse
    {
        public int OrganizationId { get; set; }
        public string OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public long UserId { get; set; }
        public string UserGuid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string SecretKey { get; set; }
        public string SecurityKey { get; set; }
        public string AccessToken { get; set; }
        //public string RefreshToken { get; set; }
        public string RoleName { get; set; }
    }
}
