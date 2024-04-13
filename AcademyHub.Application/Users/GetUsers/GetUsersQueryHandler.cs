using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.Users;
using AcademyHub.Application.Users.Models;

namespace AcademyHub.Application.Users.GetUsers;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PaginationResult<UserViewModel>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<UserViewModel>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var paginationUsers = await _userRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var usersViewModel = _mapper.Map<List<UserViewModel>>(paginationUsers.Data);

        var paginationUsersViewModel = paginationUsers.Map(usersViewModel);

        return Result.Ok(paginationUsersViewModel);
    }
}