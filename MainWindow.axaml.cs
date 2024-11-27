using Avalonia.Controls;
using demo_tab_21_11_2024.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace demo_tab_21_11_2024
{
    public partial class MainWindow : Window
    {
        List<User> _Users = DBHelper.DBContext.Users.Include(x => x.RoleNavigation).ToList();
        User GuestUser = new User() { Role = 4 };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Guest(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow(GuestUser);
            listWindow.Show();
            Close();
        }

        private void Login(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (tbox_login.Text != "" && tbox_password.Text != "" && tbox_login.Text != null && tbox_password.Text != null)
            {
                if(_Users.Where(x => x.Login == tbox_login.Text).First().Password == tbox_password.Text)
                {
                    ListWindow listWindow = new ListWindow(_Users.Where(x => x.Login == tbox_login.Text).First());
                    listWindow.Show();
                    Close();
                }
            }
        }
    }
}