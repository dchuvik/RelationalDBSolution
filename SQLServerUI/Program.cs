using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace SQLServerUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = GetConnectionString();

            SqlCrudOperation sqlCrudOperation = new SqlCrudOperation(connectionString);

            ReadAllContacts(sqlCrudOperation);


            Console.ReadLine();
        }

        private static void ReadAllContacts(SqlCrudOperation sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}