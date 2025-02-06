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

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task DeleteFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.DeleteFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<List<FriendResponse>> BySearchAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var friends = await _friendShipService.BySearchAsync(userId, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(friends);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<List<FriendResponse>> GetSentRequestAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var requests = await _friendShipService.GetSentRequestAsync(userId, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<List<FriendResponse>> GetReceivedRequestAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var requests = await _friendShipService.GetReceivedRequestAsync(userId, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task SendFriendRequestAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.SendRequestAsync(request.UserId, request.FriendId, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<bool> IsFriendAsync([FromQuery] Guid userId, [FromQuery] Guid friendId, CancellationToken cancellationToken)
        {
            return await _friendShipService.IsFriendAsync(userId, friendId, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<bool> HasSentRequestAsync([FromQuery] Guid userId, [FromQuery] Guid friendId, CancellationToken cancellationToken)
        {
          return await _friendShipService.HasSentRequestAsync(userId, friendId, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FriendShipNotFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task AcceptFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.AddFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FriendShipNotFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task RejectFriendAsync([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.RejectFriendAsync(request.UserId, request.FriendId, cancellationToken);
        }
    }
}




