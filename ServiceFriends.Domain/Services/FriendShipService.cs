using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Exceptions;
using ServiceFriends.Domain.Interfaces;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.Domain.Services
{
    public class FriendShipService
    {
        private readonly IFriendShipRepository _friendShipRepository;
        private readonly ISentFriendRequestRepository _sentFriendRequestRepository;
        private readonly IReceivedFriendRequestRepository _receivedFriendRequestRepository;
        public FriendShipService(
            IFriendShipRepository friendShipRepository,
            ISentFriendRequestRepository sentFriendRequestRepository,
            IReceivedFriendRequestRepository receivedFriendRequestRepository)
        {
            _friendShipRepository = friendShipRepository
                ?? throw new ArgumentException(nameof(friendShipRepository));
            _sentFriendRequestRepository = sentFriendRequestRepository
                 ?? throw new ArgumentException(nameof(sentFriendRequestRepository));
            _receivedFriendRequestRepository = receivedFriendRequestRepository
                ?? throw new ArgumentException(nameof(receivedFriendRequestRepository));
        }
        public async Task<List<FriendShip>> BySearchAsync
            (Guid userId, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await _friendShipRepository.BySearch(userId, options, cancellationToken);
        }

        public async Task DeleteFriendAsync
            (FriendShip friendShip, CancellationToken cancellationToken)
        {
            var existedFriendShip =
                await _friendShipRepository.FindFriendsAsync
                (friendShip.UserId, friendShip.FriendId, cancellationToken)
                ?? throw new FriendShipNotFoundException("Нет такой заявки на дружбу.");

            await _friendShipRepository.Delete(existedFriendShip, cancellationToken);
        }

        public async Task SendRequestAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var sentRequest = new SentFriendRequest(Guid.NewGuid(), userId, friendId)
            {
                CreatedAt = DateTime.UtcNow
            };

            var receiverRequest = new ReceivedFriendRequest(Guid.NewGuid(), friendId, userId)
            {
                CreatedAt = DateTime.UtcNow
            };

            await _sentFriendRequestRepository.Add(sentRequest, cancellationToken);
            await _receivedFriendRequestRepository.Add(receiverRequest, cancellationToken);
        }

        public async Task<bool> IsFriendAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await _friendShipRepository.IsFriendAsync(userId, friendId, cancellationToken);
        }

        public async Task<List<SentFriendRequest>> GetSentRequestAsync
            (Guid userId, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await _sentFriendRequestRepository
                .GetSentRequestAsync(userId, options, cancellationToken);
        }

        public async Task<List<ReceivedFriendRequest>> GetReceivedRequestAsync
            (Guid userId, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await _receivedFriendRequestRepository
                .GetReceivedRequestAsync(userId, options, cancellationToken);
        }

        public async Task<bool> HasSentRequestAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await _sentFriendRequestRepository
                .HasSentRequestAsync(userId, friendId, cancellationToken);
        }

        public async Task AddFriendAsync(FriendShip friendShip, CancellationToken cancellationToken)
        {
            var (receivedRequest, sentequest) =
                  await GetFrinedShip(friendShip.UserId, friendShip.FriendId, cancellationToken);

            friendShip.CreatedAt = DateTime.UtcNow; 
            await _receivedFriendRequestRepository.Delete(receivedRequest, cancellationToken);
            await _sentFriendRequestRepository.Delete(sentequest, cancellationToken);
            await _friendShipRepository.Add(friendShip, cancellationToken);
        }

        public async Task RejectFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var (receivedRequest, sentequest) = 
                await GetFrinedShip(userId, friendId, cancellationToken);
           
            await _receivedFriendRequestRepository.Delete(receivedRequest, cancellationToken);
            await _sentFriendRequestRepository.Delete(sentequest, cancellationToken);
        }

        private async Task<(ReceivedFriendRequest receivedRequest, SentFriendRequest sentRequest)> 
            GetFrinedShip(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var receivedRequest = await _receivedFriendRequestRepository
                .FindReceivedRequestAsync(userId, friendId, cancellationToken)
                ?? throw new FriendShipNotFoundException("Нет такой заявки на дружбу.");

            var sentequest = await _sentFriendRequestRepository
                .FindSentRequestAsync(userId, friendId, cancellationToken)
                ?? throw new FriendShipNotFoundException("Нет такой заявки на дружбу.");

            return (receivedRequest, sentequest);
        }
    }
}
