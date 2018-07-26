using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Models.Enums;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var userName = data[0];
            var albumTitle = data[1];
            var color = data[2];
            var tags = data.Skip(3).ToArray();

            var userExists = this.userService.Exists(userName);
            if (!userExists)
            {
                throw new ArgumentException($"User {userName} not found!");
            }

            var albumExists = this.albumService.Exists(albumTitle);
            if (albumExists)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var isValidColor = Enum.TryParse(color, out Color result);
            if (!isValidColor)
            {
                throw new ArgumentException($"Color {color} not found!");
            }

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].ValidateOrTransform();

                var currentTag = this.tagService.Exists(tags[i]);

                if (!currentTag)
                {
                    throw new ArgumentException("Invalid tags!");
                }
            }

            var userId = this.userService.ByUsername<UserDto>(userName).Id;
            this.albumService.Create(userId, albumTitle, color, tags);
            return $"Album {albumTitle} successfully created!";
        }
    }
}