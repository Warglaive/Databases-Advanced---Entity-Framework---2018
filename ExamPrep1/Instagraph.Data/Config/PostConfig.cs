using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(c => c.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(x => x.PostId);
        }
    }
}