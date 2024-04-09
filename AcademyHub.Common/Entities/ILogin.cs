using AcademyHub.Common.ValueObjects;

namespace AcademyHub.Common.Entities;

public interface ILogin
{
    public Password Password { get; }
}