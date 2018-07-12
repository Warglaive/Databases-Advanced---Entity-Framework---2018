using System;

namespace P03_FootballBetting.Models
{
  public  class Bet
    {
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public decimal Prediction { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}