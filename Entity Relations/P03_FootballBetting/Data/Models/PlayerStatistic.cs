﻿using System;
using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public PlayerStatistic()
        {
            this.Players = new List<Player>();
            this.Games = new List<Game>();
        }
        public Game Game { get; set; }
        public int GameId { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public int ScoredGoals { get; set; }
        public int Assists { get; set; }
        public DateTime MinutesPlayed { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}