using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(x => new
            {
                x.UserId,
                x.FollowerId
            });
        }
    }
}