using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;
        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        public string Execute(string[] data)
        {
            var userName = data[0];
            var property = data[1];
            var value = data[2];

            if (!this.userService.Exists(userName))
            {
                throw new ArgumentException($"User {userName} not found!");
            }

            var userId = this.userService.ByUsername<UserDto>(userName).Id;

            if (property == "Password")
            {
                SetPassword(userId, value);
            }
            else if (property == "BornTown")
            {
                SetBornTown(userId, value);
            }
            else if (property == "CurrentTown")
            {
                SetCurrentTown(userId, value);
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            return $"User {userName} {property} is {value}.";
        }

        private void SetCurrentTown(int userId, string name)
        {
            if (!this.townService.Exists(name))
            {
                throw new ArgumentException($"Value {name} not valid.\n {name} not found!");
            }

            var townId = this.townService.ByName<TownDto>(name).Id;
            this.userService.SetCurrentTown(userId, townId);
        }

        private void SetBornTown(int userId, string name)
        {
            if (!this.townService.Exists(name))
            {
                throw new ArgumentException($"Value {name} not valid.\n {name} not found!");
            }

            var townId = this.townService.ByName<TownDto>(name).Id;
            this.userService.SetBornTown(userId, townId);
        }

        private void SetPassword(int userId, string password)
        {
            var isLower = password.Any(c => char.IsLower(c));
            var isDigit = password.Any(c => char.IsDigit(c));

            if (!isLower || !isDigit)
            {
                throw new ArgumentException($"Value {password} not valid.\nInvalid Password");
            }
            this.userService.ChangePassword(userId, password);
        }
    }
}
