using System.Diagnostics.Eventing.Reader;
using Npgsql;

namespace UserDesktopApp;

public class UserRepository
{
    private readonly DbHelper _db;

    public UserRepository(DbHelper db) => _db = db;
    
    public void InsertUser(User user)
    {
        using var conn = _db.Connection();
        conn.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO users (login, pass, email) VALUES (@login, @pass, @email)", conn);
        cmd.Parameters.AddWithValue("login", user.Login);
        cmd.Parameters.AddWithValue("pass", user.Pass);
        cmd.Parameters.AddWithValue("email", user.Email);
        cmd.ExecuteNonQuery();
    }

    public User GetUserByLoginAndPass(string login, string pass)
    {
        using var conn = _db.Connection();
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT id, login, pass, email FROM users WHERE login = @login AND pass = @pass", conn); //@ - SQL-injection
        cmd.Parameters.AddWithValue("login", login);
        cmd.Parameters.AddWithValue("pass", pass);
        
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Login = reader.GetString(1),
                Pass = reader.GetString(2),
                Email = reader.GetString(3)
            };
        }
        return null;
    }
}