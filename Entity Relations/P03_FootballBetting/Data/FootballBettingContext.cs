﻿using Microsoft.EntityFrameworkCore;
namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected FootballBettingContext()
        {
        }
    }
}