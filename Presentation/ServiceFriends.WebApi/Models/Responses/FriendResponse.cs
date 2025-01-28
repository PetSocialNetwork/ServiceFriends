#pragma warning disable CS8618  
using ServiceFriends.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceFriends.WebApi.Models.Responses
{
    public class FriendResponse
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public Guid FriendId { get; init; }
        [Required]
        public FriendStatus Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
