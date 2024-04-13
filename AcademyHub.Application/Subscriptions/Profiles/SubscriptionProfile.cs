using AutoMapper;

using AcademyHub.Domain.Subscriptions;
using AcademyHub.Application.Subscriptions.Models;
using AcademyHub.Application.Subscriptions.UpdateSubscription;

namespace AcademyHub.Application.Subscriptions.Profiles;

internal sealed class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription, SubscriptionDetailsViewModel>();
        CreateMap<Subscription, SubscriptionViewModel>();
        CreateMap<UpdateSubscriptionInputModel, UpdateSubscriptionCommand>();
    }
}