using AccountWebApi.Model;

namespace AccountWebApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
