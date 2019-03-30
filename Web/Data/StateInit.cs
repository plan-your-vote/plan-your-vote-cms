using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingModelLibrary.Models;

namespace Web.Data
{
    public static class StateInit
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.StateSingleton.Any())
            {
                State state = new State
                {
                    StateId = State.STATE_ID,
                    currentElection = 1,
                };
                context.Add(state);
                context.SaveChanges();
            }
        }
    }
}