using System.Text.RegularExpressions;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Common.ValueObjects;

public sealed record Password
{
    public string Content { get; } = string.Empty;

    private const string _pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[!?@#$&*])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";

    private Password() { }

    private Password(string password)
    {
        Content = password;
    }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result.Fail<Password>(PasswordErrors.PasswordIsRequired);

        if (!IsPasswordValid(password))
            return Result.Fail<Password>(PasswordErrors.PasswordIsInvalid);

        var passwordHash = PasswordHashGenerator.ComputeSha256Hash(password);

        var passwordContent = new Password(passwordHash);

        return Result<Password>.Ok(passwordContent);
    }

    public override string ToString()
    {
        return Content;
    }

    private static bool IsPasswordValid(string number) =>
        Regex.IsMatch(number, _pattern);
}

public sealed record PasswordErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error PasswordIsRequired =
        new("Password.PasswordIsRequired", "Password is required", ErrorType.Validation);

    public static readonly Error PasswordIsInvalid =
        new("Password.PasswordIsInvalid", "Password is not a valid", ErrorType.Validation);
}