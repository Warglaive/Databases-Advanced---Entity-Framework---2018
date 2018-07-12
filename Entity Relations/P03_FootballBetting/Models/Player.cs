using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int SquadNumber { get; set; }
        public int TeamId { get; set; }
        public int PositionId { get; set; }
        public bool IsInjured { get; set; }
    }
}