using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Users;

namespace AcademyHub.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _userRepository.IsUniqueAsync(request.Cpf, request.Email, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(UserErrors.IsNotUnique);

        var userResult = User.Create(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Cpf,
            request.Email,
            request.Telephone,
            request.Password,
            AuthConstants.Client);

        if (!userResult.Success)
            return Result.Fail<Guid>(userResult.Errors);

        _userRepository.Create(userResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(UserErrors.CannotBeCreated);

        return Result.Ok(userResult.Value.Id);
    }
}