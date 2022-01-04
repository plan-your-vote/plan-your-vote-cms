using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public static class StateInit
    {
        public static void Initialize(ApplicationDbContext context, IConfiguration configuration)
        {
            if (!context.StateSingleton.Any())
            {
                int runningElectionID = 1;
                int managedElectionID = 1;

                if (configuration["StartupData:RunningElectionID"] != null)
                    runningElectionID = Convert.ToInt32(configuration["StartupData:RunningElectionID"]);

                if (configuration["StartupData:ManagedElectionID"] != null)
                    managedElectionID = Convert.ToInt32(configuration["StartupData:ManagedElectionID"]);

                State state = new State
                {
                    RunningElectionID = runningElectionID,
                    ManagedElectionID = managedElectionID,
                };
                context.Add(state);
                context.SaveChanges();
            }
        }
    }
}