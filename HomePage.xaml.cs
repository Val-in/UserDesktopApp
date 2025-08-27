using System.Windows;

namespace UserDesktopApp;

public partial class HomePage : Window
{
    public readonly DbHelper _db;

    public HomePage(DbHelper db)
    {
        InitializeComponent();
        _db = db;

        List<User> users = _db.GetUsers();
        listOfUsers.ItemsSource = users;
    }
    
    
}