
using System;

namespace Web
{
    public class CheckDB
    {

        public void checkType()
        {

            //Get the environment variable, assigned to a string
            string _db = Environment.GetEnvironmentVariable("APPSETTING_DB_TYPE");
            //string _db = appSettings.Value.DB_TYPE;


            //string _db = Configuration.GetConnectionString("")


            //Assign database type based on environment variable

            if (_db == "sqlite") //If DB_TYPE is sqlite
            {
                Console.WriteLine("///244/// SQLITE");
                //Code if sqlite
            }
            else if (_db == "mssql") //If DB_TYPE is MSSQL
            {
                Console.WriteLine("///244/// MSSQL");
                //Code if MSSQL
            }
            else //If DB_TYPE is MySQL, default
            {
                Console.WriteLine("///244/// MYSQL");
                //Code if MySQL
            }
        }
    }
}
