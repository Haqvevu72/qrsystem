using qrsystem.Models.Entities;

namespace qrsystem.Services.JWT;

public interface ITokenService
{
    string GenerateToken(User user);
}