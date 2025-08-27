using System.Windows;
using System.Windows.Media;

namespace UserDesktopApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window 
{
    private readonly DbHelper _db;
    public MainWindow() 
    {
        InitializeComponent();
        _db = new DbHelper(); 
    }

    private void ClickToRegister(object sender, RoutedEventArgs e)
    {
        var repo = new UserRepository(_db); 
        
        string login = TextBoxLogin.Text.Trim();
        string password = TextBoxPassword.Password.Trim();
        string passwordAgain = TextBoxPasswordAgain.Password.Trim();
        string email = TextBoxEmail.Text.Trim().ToLower();
        
    bool loginOk = false;
    bool passwordOk = false;
    bool passwordAgainOk = false;
    bool emailOk = false;
    
    if (string.IsNullOrEmpty(login) || login.Length < 5)
    {
        TextBoxLogin.ToolTip = "Login should be at least 5 characters!";
        TextBoxLogin.Background = Brushes.LightPink;
    }
    else
    {
        TextBoxLogin.ToolTip = null;
        TextBoxLogin.Background = Brushes.White;
        loginOk = true;
    }
    
    if (password.Length < 8)
    {
        TextBoxPassword.ToolTip = "Password should be at least 8 characters!";
        TextBoxPassword.Background = Brushes.LightPink;
    }
    else
    {
        TextBoxPassword.ToolTip = null;
        TextBoxPassword.Background = Brushes.White;
        passwordOk = true;
    }
    
    if (password != passwordAgain)
    {
        TextBoxPasswordAgain.ToolTip = "Passwords do not match!";
        TextBoxPasswordAgain.Background = Brushes.LightPink;
    }
    else
    {
        TextBoxPasswordAgain.ToolTip = null;
        TextBoxPasswordAgain.Background = Brushes.White;
        passwordAgainOk = true;
    }
    
    if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
    {
        TextBoxEmail.ToolTip = "Incorrect email!";
        TextBoxEmail.Background = Brushes.LightPink;
    }
    else
    {
        TextBoxEmail.ToolTip = null;
        TextBoxEmail.Background = Brushes.White;
        emailOk = true;
    }
    
    if (loginOk && passwordOk && passwordAgainOk && emailOk)
    {
        try
        {
            repo.InsertUser(new User(login, password, email));
            MessageBox.Show("User is saved!");
            Authentication auth = new Authentication();
            auth.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error with saving: " + ex.Message);
        }
    }
    else
    {
        MessageBox.Show("Data is incorrect!");
    }
}

    private void LogInButton(object sender, RoutedEventArgs e)
    {
        Authentication auth = new Authentication();
        auth.Show();
        this.Close();
    }
}