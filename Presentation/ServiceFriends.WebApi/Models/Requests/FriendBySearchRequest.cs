using System.ComponentModel.DataAnnotations;

namespace ServiceFriends.WebApi.Models.Requests
{
    public class FriendBySearchRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Take { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Offset { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
