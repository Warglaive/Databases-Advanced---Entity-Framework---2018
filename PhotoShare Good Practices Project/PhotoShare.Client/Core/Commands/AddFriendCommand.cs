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

            if (!userExists || !friendExistrs)
            {
                throw new ArgumentException($"{}");
            }

        }
    }
}
