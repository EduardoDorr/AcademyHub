using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.Users;
using AcademyHub.Application.Users.Models;

namespace AcademyHub.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDetailsViewModel?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDetailsViewModel?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return Result.Fail<UserDetailsViewModel?>(UserErrors.NotFound);

        var userViewModel = _mapper.Map<UserDetailsViewModel?>(user);

        return Result.Ok(userViewModel);
    }
}