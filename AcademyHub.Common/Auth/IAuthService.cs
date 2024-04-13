namespace AcademyHub.Common.Auth;

public interface IAuthService
{
    string ComputeSha256Hash(string password);
    string GenerateJwtToken(string email, string role);
}