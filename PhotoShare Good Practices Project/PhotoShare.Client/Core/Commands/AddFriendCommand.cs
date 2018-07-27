using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;
        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var friendUsername = data[1];
            var userExists = this.userService.Exists(username);
            var friendExistrs = this.userService.Exists(friendUsername);

            if (!userExists)
            {
                throw new ArgumentException($"{username} not found!");
            }
            if (!friendExistrs)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);
            var friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            var isSentRequestFromFriend = friend.Friends.Any(n => n.Username == user.Username);
            var isSentRequestFromUser = user.Friends.Any(n => n.Username == friend.Username);

            if (isSentRequestFromFriend && isSentRequestFromUser)
            {
                throw new ArgumentException($"{friend.Username} is already a friend to {user.Username}");
            }
            else if (isSentRequestFromUser && !isSentRequestFromFriend)
            {
                throw new InvalidOperationException("Request is already send!");
            }
            else if (isSentRequestFromFriend && !isSentRequestFromUser)
            {
                throw new InvalidOperationException("Request is already send!");
            }
            this.userService.AddFriend(user.Id, friend.Id);
            return $"Friend {friend} added to {user}";
        }
    }
}
