using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.Domain.Users;

namespace AcademyHub.Application.Users.LoginUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserViewModel?>>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<Result<LoginUserViewModel?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

        if (user is null)
            return Result.Fail<LoginUserViewModel?>(UserErrors.NotFound);

        var token = _authService.GenerateJwtToken(user.Email.Address, user.Role);

        var loginUser = new LoginUserViewModel(user.Email.Address, token);

        return Result.Ok(loginUser);
    }
}
