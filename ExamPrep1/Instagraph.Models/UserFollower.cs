using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class UserFollower
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        [Key]
        public int FollowerId { get; set; }
        public User Follower { get; set; }
    }
}