using System.Windows;
using System.Windows.Media;

namespace UserDesktopApp;

public partial class Authentication : Window
{
    private readonly DbHelper _db;
    
    public Authentication()
    {
        InitializeComponent();
        _db = new DbHelper(); 
    }

    private void ClickToAuthenticate(object sender, RoutedEventArgs e)
    {
        var repo = new UserRepository(_db); // _db to UserRepository, can get the methods from DbHelper
        
        string login = TextBoxLogin.Text.Trim();
        string password = TextBoxPassword.Password.Trim();
        
        var user = repo.GetUserByLoginAndPass(login, password);
        if (user == null)
        {
            TextBoxLogin.ToolTip = "Your login or password is incorrect!";
            TextBoxLogin.Background = Brushes.LightPink;
            TextBoxPassword.Background = Brushes.LightPink;
            MessageBox.Show("Your data is incorrect!");
            return;
        }
        
        TextBoxLogin.ToolTip = null;
        TextBoxLogin.Background = Brushes.White;
        TextBoxPassword.Background = Brushes.White;
        MessageBox.Show("Welcome, " + user.Login + "!");
        HomePage auth = new HomePage(_db);
        auth.Show();
        this.Close();
    }

    private void RegistrationButton(object sender, RoutedEventArgs e)
    {
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();
    }
}