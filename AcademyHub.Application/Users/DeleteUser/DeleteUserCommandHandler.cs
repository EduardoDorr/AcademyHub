using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Users;

namespace AcademyHub.Application.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return Result.Fail(UserErrors.NotFound);

        user.Deactivate();

        _userRepository.Update(user);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(UserErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
