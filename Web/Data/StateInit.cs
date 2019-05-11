using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

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
                    RunningElectionID = 1,
                    ManagedElectionID = 1,
                };
                context.Add(state);
                context.SaveChanges();
            }
        }
    }
}