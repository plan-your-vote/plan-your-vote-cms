
using System;

namespace Web
{
    public class CheckDB
    {

        public String checkType()
        {

            //Get the environment variable, assigned to a string
            string _db = Environment.GetEnvironmentVariable("APPSETTING_DB_TYPE");
            //string _db = appSettings.Value.DB_TYPE;
            return _db;
        }

        public String getConnectionStringEnvVar()
        {
            return Environment.GetEnvironmentVariable("APPSETTING_CONNECTION_STRING");
        }
    }
}
