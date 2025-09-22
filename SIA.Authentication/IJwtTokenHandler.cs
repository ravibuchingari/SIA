
namespace Authentication.JWTAuthenticationManager
{
    public interface IJwtTokenHandler
    {
        Task<AuthenticationResponse> GenerateToken(AuthenticationResponse authenticationResponse, bool isTemporary = false);
    }
}