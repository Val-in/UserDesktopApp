namespace UserDesktopApp;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Pass { get; set; }
    public string Email { get; set; }

    public User() { }
    
    public User(string  login, string pass, string email)
    {
        this.Login = login;
        this.Pass = pass;
        this.Email = email;
    }

    public override string ToString()
    {
        return "User: login=" + Login + ", email=" + Email;
    }
}