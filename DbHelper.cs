using System.Configuration;
using Npgsql;

namespace UserDesktopApp;

public class DbHelper 
{
    private readonly string _connectionString;

    public DbHelper()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
    }
    
    public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(_connectionString);
    }
    
    public List<User> GetUsers()
    {
        var users = new List<User>();

        using (var conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
            string sql = "SELECT Id, Login, Email FROM Users";

            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Email = reader.GetString(2)
                    });
                }
            }
        }

        return users;
    }
}