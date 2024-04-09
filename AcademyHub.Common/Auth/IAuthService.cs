namespace AcademyHub.Common.Auth;

public interface IAuthService
{
    string GenerateJwtToken(string email, string role);
    string ComputeSha256Hash(string password);
}