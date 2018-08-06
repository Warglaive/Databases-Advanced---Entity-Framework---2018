using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos.Import;
using Instagraph.Models;
using ValidationContext = AutoMapper.ValidationContext;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var deserializePictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);
            var pictures = new List<Picture>();
            var sb = new StringBuilder();
            foreach (var picture in deserializePictures)
            {
                if (IsValid(picture))
                {
                    pictures.Add(picture);
                    sb.AppendLine($"Successfully imported Picture {picture.Path}.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var deserializeUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);
            var users = new List<User>();
            var sb = new StringBuilder();
            foreach (var userDto in deserializeUsers)
            {
                // var mapper = Mapper.Configuration.CreateMapper();

                var user = Mapper.Map<User>(userDto);

                var profilePicture = context.Pictures.FirstOrDefault(x => x.Path == userDto.ProfilePicture);
                user.ProfilePicture = profilePicture;

                if (IsValid(user))
                {
                    users.Add(user);
                    sb.AppendLine($"Successfully imported User {userDto.Username}.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.Users.AddRange(users);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
        //if not working in judge go for IsValid with validator
        public static bool IsValid(object deserializedUser)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(deserializedUser);
            var result = new List<ValidationResult>();
            return Validator.TryValidateObject(deserializedUser, validationContext, result, true);
        }
    }
}