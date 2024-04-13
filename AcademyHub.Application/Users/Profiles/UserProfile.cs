using AutoMapper;

using AcademyHub.Domain.Users;
using AcademyHub.Application.Users.Models;
using AcademyHub.Application.Users.UpdateUser;

namespace AcademyHub.Application.Users.Profiles;

internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDetailsViewModel>();
        CreateMap<User, UserViewModel>();
        CreateMap<UpdateUserInputModel, UpdateUserCommand>();
    }
}