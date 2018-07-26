using System.IO;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;
        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            var userName = data[0];
            var password = data[1];
            var repeatPassword = data[2];
            var email = data[3];

            var registerUserDto = new RegisterUserDto
            {
                Username = userName,
                Password = password,
                Email = email
            };
            if (!IsValid(registerUserDto))
            {
                throw new ArgumentException("invalid data");
            }

            var userExists = this.userService.Exists(userName);

            if (userExists)
            {
                throw new InvalidOperationException($"Username {userName} is already taken!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }
            this.userService.Register(userName, password, email);
            return $"User {userName} was registered successfully!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
