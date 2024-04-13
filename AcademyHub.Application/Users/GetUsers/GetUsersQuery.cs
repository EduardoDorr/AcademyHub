using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.Users.Models;

namespace AcademyHub.Application.Users.GetUsers;

public sealed record GetUsersQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<UserViewModel>>>;