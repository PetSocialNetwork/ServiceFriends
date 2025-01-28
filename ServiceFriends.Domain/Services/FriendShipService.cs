﻿using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Enums;
using ServiceFriends.Domain.Exceptions;
using ServiceFriends.Domain.Interfaces;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace ServiceFriends.Domain.Services
{
    public class FriendShipService
    {
        private readonly IFriendShipRepository _friendShipRepository;
        public FriendShipService(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository
                ?? throw new ArgumentException(nameof(friendShipRepository));
        }

        public async Task SendRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var friends = new ConcurrentBag<FriendShip>();
            var userFriend = new FriendShip(Guid.NewGuid(), userId, friendId)
            {
                CreatedAt = DateTime.UtcNow,
                Status = FriendStatus.Sent
            };
            friends.Add(userFriend);
            var friend = new FriendShip(Guid.NewGuid(), friendId, userId)
            {
                CreatedAt = DateTime.UtcNow,
                Status = FriendStatus.Received
            };
            friends.Add(friend);

            await _friendShipRepository.AddRange(friends, cancellationToken);
            //Подумать, делаем ли какое- уведомление у пользователя
        }

        public async Task AddFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var friends = new ConcurrentBag<FriendShip>();
            var (existedUser, existedFriend) = await GetFrinedShip(userId, friendId, cancellationToken);

            existedUser.Status = FriendStatus.Accepted;
            existedUser.CreatedAt = DateTime.UtcNow;

            existedFriend.Status = FriendStatus.Accepted;
            existedFriend.CreatedAt = DateTime.UtcNow;

            friends.Add(existedUser);
            friends.Add(existedFriend);

            await _friendShipRepository.UpdateRange(friends, cancellationToken);
        }

        public async Task RejectFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var friends = new ConcurrentBag<FriendShip>();
            var (existedUser, existedFriend) = await GetFrinedShip(userId, friendId, cancellationToken);

            existedUser.Status = FriendStatus.Rejected;
            existedUser.CreatedAt = DateTime.UtcNow;

            existedFriend.Status = FriendStatus.Rejected;
            existedFriend.CreatedAt = DateTime.UtcNow;

            friends.Add(existedUser);
            friends.Add(existedFriend);

            await _friendShipRepository.UpdateRange(friends, cancellationToken);
        }

        public async Task DeleteFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var friends =
                await _friendShipRepository.FindFriendsAsync
                (userId, friendId, cancellationToken);

            if (friends != null && friends.Count != 0)
            {
                await _friendShipRepository.DeleteRange(friends, cancellationToken);
            }
        }

        public async IAsyncEnumerable<FriendShip> BySearchAsync(Guid userId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var friend in _friendShipRepository.BySearch(userId, cancellationToken))
                yield return friend;
        }

        //получить список входящих заявок, то есть те, у которых статус Sent
        public async Task<List<FriendShip>> GetSentRequestAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _friendShipRepository.GetSentRequestAsync(userId, cancellationToken);
        }

        //получить список отправленных заявок, то есть те, у которых статус Received
        public async Task<List<FriendShip>> GetReceivedRequestAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _friendShipRepository.GetReceivedRequestAsync(userId, cancellationToken);
        }

        private async Task<(FriendShip user, FriendShip friend)> GetFrinedShip(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var existedUser = await _friendShipRepository.FindReceivedSentAsync(userId, friendId, cancellationToken);
            if (existedUser == null)
            {
                throw new FriendShipNotFoundException("Нет такой завяки на дружбу.");
            }
            var existedFriend = await _friendShipRepository.FindReceivedRequestAsync(userId, friendId, cancellationToken);
            if (existedFriend == null)
            {
                throw new FriendShipNotFoundException("Нет такой завяки на дружбу.");
            }

            return (existedUser, existedFriend);
        }
    }
}
