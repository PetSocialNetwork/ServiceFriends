using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Services;
using ServiceFriends.Domain.Shared;
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
            _friendShipService = friendShipService 
                ?? throw new ArgumentNullException(nameof(friendShipService));
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Возвращает всех друзей
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<FriendResponse>> BySearchAsync
            ([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var options = _mapper.Map<PaginationOptions>(request.Options);
            var friends = await _friendShipService.BySearchAsync
                (request.UserId, options, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(friends);
        }

        /// <summary>
        /// Возвращает отправленные заявки в друзья
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<FriendResponse>> GetSentRequestAsync
            ([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var options = _mapper.Map<PaginationOptions>(request.Options);
            var requests = await _friendShipService.GetSentRequestAsync
                (request.UserId, options, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        /// <summary>
        /// Возвращает полученные заявки в друзья
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<FriendResponse>> GetReceivedRequestAsync
            ([FromBody] FriendBySearchRequest request, CancellationToken cancellationToken)
        {
            var options = _mapper.Map<PaginationOptions>(request.Options);
            var requests = await _friendShipService.GetReceivedRequestAsync
                (request.UserId, options, cancellationToken);
            return _mapper.Map<List<FriendResponse>>(requests);
        }

        /// <summary>
        /// Удаляет пользователя из друзей
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task DeleteFriendAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            var friendShip = _mapper.Map<FriendShip>(request);
            await _friendShipService.DeleteFriendAsync(friendShip, cancellationToken);
        }

        /// <summary>
        /// Принимает заявку в друзья
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPut("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task AcceptFriendAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            var friendShip = _mapper.Map<FriendShip>(request);
            await _friendShipService.AddFriendAsync(friendShip, cancellationToken);
        }

        /// <summary>
        /// Отправляет заявку на дружбу
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task SendFriendRequestAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.SendRequestAsync
                (request.UserId, request.FriendId, cancellationToken);
        }

        /// <summary>
        /// Проверяет, является ли пользователем твои другом
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="friendId">Идентификатор друга</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<bool> IsFriendAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            return await _friendShipService.IsFriendAsync
                (request.UserId, request.FriendId, cancellationToken);
        }

        /// <summary>
        /// Проверяет, есть ли уже отправленная заявку на дружбу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="friendId">Идентификатор друга</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<bool> HasSentRequestAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            return await _friendShipService.HasSentRequestAsync
                (request.UserId, request.FriendId, cancellationToken);
        }

        /// <summary>
        /// Отклоняет заявку в друзья
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPut("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task RejectFriendAsync
            ([FromBody] FriendRequest request, CancellationToken cancellationToken)
        {
            await _friendShipService.RejectFriendAsync
                (request.UserId, request.FriendId, cancellationToken);
        }
    }
}




