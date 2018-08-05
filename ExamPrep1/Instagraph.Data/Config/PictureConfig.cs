using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Picture)
                .HasForeignKey(x => x.PictureId);
        }
    }
}