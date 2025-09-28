namespace SIA.Domain.Entities
{
    public class RefreshTokenVM
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now >= Expires;
        public DateTime Created { get; set; }
    }
}
