#pragma warning disable CS8618 
using System.ComponentModel.DataAnnotations;

namespace ServiceFriends.WebApi.Models.Requests
{
    public class FriendRequest
    {
        [Required]
        public Guid UserId { get; init; }
        [Required]
        public Guid FriendId { get; init; }
    }
}
