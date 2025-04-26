using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFriends.Domain.Services;
using ServiceFriends.WebApi.Models.Requests;
using ServiceFriends.WebApi.Models.Responses;

namespace ServiceFriends.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipController : ControllerBase
    {
        private readonly FriendShipService _friendShipService;
        private readonly IMapper _mapper;
        public FriendShipController(FriendShipService friendShipService,
            IMapper mapper)
        {
            _friendShipService = friendShipService ?? throw new ArgumentNullException(nameof(friendShipService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("[action]")]
        public async Task<List<FriendResponse>> BySearchAsync([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var friends = await _friendShipService.BySearchAsync(request.UserId, request.Take, request.Offset, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(friends);
        }

        [HttpPost("[action]")]
        public async Task<List<FriendResponse>> GetSentRequestAsync([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var requests = await _friendShipService.GetSentRequestAsync(request.UserId, request.Take, request.Offset, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        [HttpPost("[action]")]
        public async Task<List<FriendResponse>> GetReceivedRequestAsync([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var requests = await _friendShipService.GetReceivedRequestAsync(request.UserId, request.Take, request.Offset, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        [HttpPost("[action]")]
        public async Task DeleteFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.DeleteFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }

        [HttpPost("[action]")]
        public async Task SendFriendRequestAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.SendRequestAsync(request.UserId, request.FriendId, cancellationToken);
        }

        [HttpGet("[action]")]
        public async Task<bool> IsFriendAsync([FromQuery] Guid userId, [FromQuery] Guid friendId, CancellationToken cancellationToken)
        {
            return await _friendShipService.IsFriendAsync(userId, friendId, cancellationToken);
        }

        [HttpGet("[action]")]
        public async Task<bool> HasSentRequestAsync([FromQuery] Guid userId, [FromQuery] Guid friendId, CancellationToken cancellationToken)
        {
          return await _friendShipService.HasSentRequestAsync(userId, friendId, cancellationToken);
        }

        [HttpPut("[action]")]
        public async Task AcceptFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.AddFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }

        [HttpPut("[action]")]
        public async Task RejectFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.RejectFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }
    }
}




