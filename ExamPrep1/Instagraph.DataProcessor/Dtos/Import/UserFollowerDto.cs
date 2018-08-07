using Instagraph.Models;

namespace Instagraph.DataProcessor.Dtos.Import
{
    public class UserFollowerDto
    {
        public string User { get; set; }
        //public User User { get; set; }

        public string Follower { get; set; }
        // public User Follower { get; set; }
    }
}