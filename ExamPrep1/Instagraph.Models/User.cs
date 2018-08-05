﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class User
    {
        public User()
        {
            this.Followers = new List<UserFollower>();
            this.UsersFollowing = new List<UserFollower>();
            this.Posts = new List<Post>();
            this.Comments = new List<Comment>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(30), Required]
        public string Username { get; set; }

        [MaxLength(20), Required]
        public string Password { get; set; }

        [Required]
        public int ProfilePictureId { get; set; }
        [Required]
        public Picture ProfilePicture { get; set; }

        public ICollection<UserFollower> Followers { get; set; }
        public ICollection<UserFollower> UsersFollowing { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}