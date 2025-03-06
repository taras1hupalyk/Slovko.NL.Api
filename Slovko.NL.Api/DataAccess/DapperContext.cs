using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;

namespace Slovko.NL.Api.DataAccess
{
    public class DapperContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public SqliteConnection Connection
        {
            get
            {
                var connection =  new SqliteConnection(_connectionString);
                connection.CreateFunction(
                "regexp",
                (string pattern, string input)
                    => Regex.IsMatch(input, pattern));

                return connection;
            }
        }
    }

}
