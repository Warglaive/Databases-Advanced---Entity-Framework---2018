using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.TableRelationsConfig
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            //homeTeam
            builder.HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId);

            builder.HasOne(x => x.HomeTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.HomeTeamId);
            //awayTeam
            builder.HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId);

            builder.HasOne(x => x.AwayTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.AwayTeamId);
            //
        }
    }
}