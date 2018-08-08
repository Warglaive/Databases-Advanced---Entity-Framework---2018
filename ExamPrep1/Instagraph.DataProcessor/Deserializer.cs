using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
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
            var deserializePictures = JsonConvert.DeserializeObject<PictureDto[]>(jsonString);
            var pictures = new List<Picture>();
            var sb = new StringBuilder();
            foreach (var pictureDto in deserializePictures)
            {
                if (IsValid(pictureDto))
                {
                    var picture = Mapper.Map<Picture>(pictureDto);
                    if (!string.IsNullOrEmpty(picture.Path) && picture.Size > 0)
                    {
                        pictures.Add(picture);
                        sb.AppendLine($"Successfully imported Picture {picture.Path}.");
                    }
                    else
                    {
                        sb.AppendLine("Error: Invalid data.");
                    }
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
            var usersFollowersDto = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var userFollowers = new List<UserFollower>();

            foreach (var userFollowerDto in usersFollowersDto)
            {
                if (IsValid(userFollowerDto))
                {
                    var user = context.Users
                        .FirstOrDefault(n => n.Username == userFollowerDto.User);

                    var followerUser = context.Users
                        .FirstOrDefault(x => x.Username == userFollowerDto.Follower);

                    //check if user or follower are already added, if so, they are tracked and exception appear
                    var areAdded = userFollowers.Any(x => x.User == user
                                                          && x.Follower == followerUser);
                    if (areAdded)
                    {
                        sb.AppendLine("Error: Invalid data.");
                        continue;
                    }
                    //
                    if (user != null && followerUser != null)
                    {
                        var follower = Mapper.Map<UserFollower>(userFollowerDto);
                        follower.User = user;
                        follower.Follower = followerUser;

                        userFollowers.Add(follower);
                        sb.AppendLine($"Successfully imported Follower {followerUser.Username} to User {user.Username}.");

                    }
                    else
                    {
                        sb.AppendLine("Error: Invalid data.");
                    }
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(PostDto[]),
                new XmlRootAttribute("posts"));

            var deserializedPosts = (PostDto[])serializer.Deserialize(new StringReader(xmlString));
            var posts = new List<Post>();
            foreach (var postDto in deserializedPosts)
            {
                if (IsValid(postDto))
                {
                    var userToCheck = context.Users.SingleOrDefault(x => x.Username == postDto.User);
                    var pictureToCheck = context.Pictures.SingleOrDefault(x => x.Path == postDto.Picture);

                    if (userToCheck == null || pictureToCheck == null)
                    {
                        sb.AppendLine("Error: Invalid data.");
                        continue;
                    }

                    var post = Mapper.Map<Post>(postDto);
                    post.User = userToCheck;
                    post.Picture = pictureToCheck;
                    posts.Add(post);
                    sb.AppendLine($"Successfully imported Post {post.Caption}.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
            }

            //Console.WriteLine(sb.ToString().Trim());
            context.Posts.AddRange(posts);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CommentDto[]), new XmlRootAttribute("comments"));
            var deserializedComments = (CommentDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validComments = new List<Comment>();
            foreach (var commentDto in deserializedComments)
            {
                var user = context.Users.SingleOrDefault(u => u.Username == commentDto.User);

                if (!IsValid(commentDto) || user == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                bool isParsed = int.TryParse(commentDto.PostId?.Id, out var parsedId);

                if (!isParsed)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var postId = context.Posts.SingleOrDefault(p => p.Id == parsedId)?.Id;
                if (postId == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                sb.AppendLine($"Successfully imported Comment {commentDto.Content}.");
                var comment = Mapper.Map<Comment>(commentDto);
                comment.User = user;
                comment.PostId = postId.Value;

                validComments.Add(comment);
            }
            context.Comments.AddRange(validComments);
            context.SaveChanges();

            return sb.ToString();
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