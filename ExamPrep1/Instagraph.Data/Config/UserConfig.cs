using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(p => p.ProfilePicture)
                .WithMany(u => u.Users)
                .HasForeignKey(x => x.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.UsersFollowing)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Followers)
                .WithOne(u => u.Follower)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            //
        }
    }
}