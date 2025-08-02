using AutoMapper;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Shared;
using ServiceFriends.WebApi.Models.Requests;
using ServiceFriends.WebApi.Models.Responses;

namespace ServiceFriends.WebApi.Mappings
{
    public class FriendsProfile : Profile
    {
        public FriendsProfile()
        {
            CreateMap<FriendShip, FriendResponse>();
            CreateMap<SentFriendRequest, FriendResponse>();
            CreateMap<ReceivedFriendRequest, FriendResponse>();

            CreateMap<FriendRequest, FriendShip>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.FriendId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<FriendRequest, FriendShip>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.FriendId));

            CreateMap<PaginationRequest, PaginationOptions>();
        }
    }
}