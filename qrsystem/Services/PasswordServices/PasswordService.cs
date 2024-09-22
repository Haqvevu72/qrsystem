using System.Security.Cryptography;
using System.Text;

namespace qrsystem.Services.AuthServices;

public class PasswordService: IPasswordService
{
    public string HashPassword(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var providedHashed = HashPassword(providedPassword);
        return hashedPassword == providedHashed;
    }
}