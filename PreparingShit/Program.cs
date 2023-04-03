

//connect to sqlite database
using Microsoft.Data.Sqlite;
using Npgsql;


var connectionString = "Host=db-postgresql-fra1-33211-do-user-13717615-0.b.db.ondigitalocean.com;" +
        "Port=25060;" +
        "Username=doadmin;" +
        "Password=AVNS_VcwP66piLiV3JOWK4YO;" +
        "Database=hts_db";
var connection = new NpgsqlConnection(connectionString);
connection.Open();




class WordInfo
{
    public int Id { get; set; }
    public string Word { get; set; }

    public double Entropy { get; set; }
}


