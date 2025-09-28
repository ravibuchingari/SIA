namespace SIA.Domain.Entities
{
    public class RefreshTokenVM
    {
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow >= Expires;
        public DateTimeOffset Created { get; set; }
        public int UserId { get; set; }

    }
}
