using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo_tab_21_11_2024.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace demo_tab_21_11_2024;

public partial class AddWindow : Window
{
    User _LoggedUser;
    Product _Product;

    public AddWindow()
    {
        InitializeComponent();
    }
    public AddWindow(User user)
    {
        _Product = new Product();
        _LoggedUser = user;
        InitializeComponent();
        grid_addwindow.DataContext = _Product;
        btn_delete.IsVisible = false;
    }
    public AddWindow(Product product, User user)
    {
        _Product = product;
        _LoggedUser = user;
        InitializeComponent();
        grid_addwindow.DataContext = _Product;
    }

    private void Return(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListWindow listWindow = new ListWindow(_LoggedUser);
        listWindow.Show();
        Close();
    }

    private void Deletion(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DBHelper.DBContext.OrderProducts.Include(x => x.ProductNavigation).Where(x => x.Product == _Product.Id).Count() == 0)
        {
            DBHelper.DBContext.Products.Remove(_Product);
            DBHelper.DBContext.SaveChanges();
            ListWindow listWindow = new ListWindow(_LoggedUser);
            listWindow.Show();
            Close();
        }
       
    }
}