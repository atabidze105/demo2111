using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo_tab_21_11_2024.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace demo_tab_21_11_2024;

public partial class CartWindow : Window
{
    Order _NewOrder;
    public CartWindow()
    {
        InitializeComponent();
        _NewOrder = new Order();
        //lbox_cart.ItemsSource = _NewOrder.OrderProducts.ToList();
        lbox_cart.ItemsSource = DBHelper.DBContext.Orders.Include(x => x.OrderProducts).ToList()[1].OrderProducts.ToList();
    }

    public CartWindow(User user,Order order)
    {
        _NewOrder = order;
        _NewOrder.Status = 1;
        
        InitializeComponent();
        lbox_cart.ItemsSource = DBHelper.DBContext.Orders.Include(x => x.OrderProducts).ToList()[1].OrderProducts.ToList();
    }


}