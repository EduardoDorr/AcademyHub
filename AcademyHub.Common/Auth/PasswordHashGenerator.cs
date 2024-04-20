using System.Text;
using System.Security.Cryptography;

namespace AcademyHub.Common.Auth;

public static class PasswordHashGenerator
{
    public static string ComputeSha256Hash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
                stringBuilder.Append(bytes[i].ToString("x2"));

            return stringBuilder.ToString().ToUpperInvariant();
        }
    }
}