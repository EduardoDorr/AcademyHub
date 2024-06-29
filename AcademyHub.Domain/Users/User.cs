using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Common.ValueObjects;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Domain.Users;

public sealed class User : BaseEntity, ILogin
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Cpf Cpf { get; private set; }
    public Email Email { get; private set; }
    public Telephone Telephone { get; private set; }
    public Password Password { get; private set; }
    public string Role { get; private set; }
    public string? PaymentGatewayClientId { get; private set; }

    public List<Enrollment> Enrollments { get; private set; } = [];
    public List<LessonFinished> LessonFinisheds { get; private set; } = [];

    protected User() { }

    private User(
        string firstName,
        string lastName,
        DateTime birthDate,
        Cpf cpf,
        Email email,
        Telephone telephone,
        Password password,
        string role)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Cpf = cpf;
        Email = email;
        Telephone = telephone;
        Password = password;
        Role = role;
    }

    public static Result<User> Create(
        string firstName,
        string lastName,
        DateTime birthDate,
        string cpf,
        string email,
        string telephone,
        string password,
        string role)
    {
        var cpfResult = Cpf.Create(cpf);

        if (!cpfResult.Success)
            return Result.Fail<User>(cpfResult.Errors);

        var emailResult = Email.Create(email);

        if (!emailResult.Success)
            return Result.Fail<User>(emailResult.Errors);

        var telephoneResult = Telephone.Create(telephone);

        if (!telephoneResult.Success)
            return Result.Fail<User>(telephoneResult.Errors);

        var passwordResult = Password.Create(password);

        if (!passwordResult.Success)
            return Result.Fail<User>(passwordResult.Errors);

        var user =
            new User(firstName,
                     lastName,
                     birthDate,
                     cpfResult.Value,
                     emailResult.Value,
                     telephoneResult.Value,
                     passwordResult.Value,
                     role);

        return Result<User>.Ok(user);
    }

    public Result Update(string firstName, string lastName, string email, string telephone)
    {
        var emailResult = Email.Create(email);

        if (!emailResult.Success)
            return Result.Fail(emailResult.Errors);

        var telephoneResult = Telephone.Create(telephone);

        if (!telephoneResult.Success)
            return Result.Fail(telephoneResult.Errors);

        FirstName = firstName;
        LastName = lastName;
        Email = emailResult.Value;
        Telephone = telephoneResult.Value;

        return Result.Ok();
    }

    public void SetPaymentGatewayClientId(string clientId) =>
        PaymentGatewayClientId = clientId;
}