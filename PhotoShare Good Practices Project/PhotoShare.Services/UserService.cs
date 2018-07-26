using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(i => i.Id == id).FirstOrDefault();

        public TModel ByUsername<TModel>(string username)
            => By<TModel>(n => n.Username == username).FirstOrDefault();

        public bool Exists(int id)
            => ById<User>(id) != null;

        public bool Exists(string name)
            => ByUsername<User>(name) != null;

        public User Register(string username, string password, string email)
        {
            var newUser = new User
            {
                Username = username,
                Password = password,
                Email = email
            };
            this.context.Users.Add(newUser);
            this.context.SaveChanges();
            return newUser;
        }

        public void Delete(string username)
        {
            var user = ByUsername<User>(username);
            this.context.Users.Remove(user);
            this.context.SaveChanges();
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var friendShip = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };
            this.context.Friendships.Add(friendShip);
            this.context.SaveChanges();
            return friendShip;
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            var friendShip = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };
            this.context.Friendships.Add(friendShip);
            this.context.SaveChanges();
            return friendShip;
        }

        public void ChangePassword(int userId, string password)
        {
            this.context.Users.Find(userId).Password = password;
            this.context.SaveChanges();
        }

        public void SetBornTown(int userId, int townId)
        {
            this.ById<User>(userId).BornTownId = townId;
            this.context.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            this.ById<User>(userId).CurrentTown.Id = townId;
            this.context.SaveChanges();
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
            => this.context.Users.Where(predicate)
                .AsQueryable().ProjectTo<TModel>();
    }
}