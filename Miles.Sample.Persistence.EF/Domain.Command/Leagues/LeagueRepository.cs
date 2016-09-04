﻿using Miles.Sample.Domain.Command.Leagues;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miles.Sample.Persistence.EF.Domain.Command.Leagues
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly SampleDbContext dbContext;

        public LeagueRepository(SampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<League> GetByAbbreviationAsync(LeagueAbbreviation abbr)
        {
            return dbContext.Leagues.SingleOrDefaultAsync(x => x.Abbreviation.Abbreviation == abbr.Abbreviation);
        }

        public Task SaveAsync(League league)
        {
            dbContext.Leagues.Add(league);
            return dbContext.SaveChangesAsync();
        }
    }
}
