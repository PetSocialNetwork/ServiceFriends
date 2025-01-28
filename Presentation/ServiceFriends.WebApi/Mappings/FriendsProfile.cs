using AutoMapper;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Enums;
using ServiceFriends.WebApi.Models.Requests;
using ServiceFriends.WebApi.Models.Responses;

namespace ServiceFriends.WebApi.Mappings
{
    public class FriendsProfile : Profile
    {
        public FriendsProfile()
        {
            CreateMap<FriendShip, FriendResponse>();

            CreateMap<FriendRequest, FriendShip>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.FriendId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => FriendStatus.Accepted))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}