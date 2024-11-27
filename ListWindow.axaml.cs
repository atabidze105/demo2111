using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using demo_tab_21_11_2024.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace demo_tab_21_11_2024;

public partial class ListWindow : Window
{
    List<Product> _Products = DBHelper.DBContext.Products.Include(x => x.CathegoryNavigation).Include(x => x.ManufacturerNavigation).Include(x => x.SupplierNavigation).OrderBy(x => x.Id).ToList();
    List<Product> _FoundedProducts = [];
    //List<Product> _Cart = [];
    Order _NewOrder = new() { Id = DBHelper.DBContext.Orders.ToList().Max(x => x.Id) + 1, Status = 1 };
    User _LoggedUser;

    public ListWindow()
    {
        InitializeComponent();
        listbox_products.ItemsSource = _Products.ToList();

    }
    public ListWindow(User user)
    {
        _LoggedUser = user;

        InitializeComponent();
        tblock_username.Text = _LoggedUser.Role != 4 ? $"Выполнен вход: {_LoggedUser.Firstname} {_LoggedUser.Name} {_LoggedUser.Lastname}" : "Выполнен вход: Гость" ;
        tblock_role.Text = _LoggedUser.Role != 4 ? _LoggedUser.RoleNavigation.Name : "";
        tblock_itemsCount.Text = $"{_Products.Count}/{_Products.Count}";
        tbox_searchbar.Text = "";

        listbox_products.ItemsSource = _Products.ToList();

        cbox_filtration.SelectedIndex = 0;
        cbox_sorting.SelectedIndex = 0;

        btn_add.IsVisible = _LoggedUser.Role == 1 ? true : false;

    }
    public ListWindow(User user, Order order)
    {
        _LoggedUser = user;
        _NewOrder = order;
        //_Cart = cart;

        InitializeComponent();
        tblock_username.Text = _LoggedUser.Role != 4 ? $"Выполнен вход: {_LoggedUser.Firstname} {_LoggedUser.Name} {_LoggedUser.Lastname}" : "Выполнен вход: Гость" ;
        tblock_role.Text = _LoggedUser.Role != 4 ? _LoggedUser.RoleNavigation.Name : "";
        tblock_itemsCount.Text = $"{_Products.Count}/{_Products.Count}";
        tbox_searchbar.Text = "";

        listbox_products.ItemsSource = _Products.ToList();

        cbox_filtration.SelectedIndex = 0;
        cbox_sorting.SelectedIndex = 0;

        btn_add.IsVisible = _LoggedUser.Role == 1 ? true : false;
        btn_openCart.IsVisible = _NewOrder.OrderProducts.Count > 0 ? true : false;
    }


    private void Return(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void Searchbar(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        UsingSearchingAndFiltration();
    }

    private void ComboBox_SelectionChangedFiltration(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        UsingSearchingAndFiltration();
    }

    private void ComboBox_SelectionChangedSorting(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        UsingSearchingAndFiltration();
    }

    private List<Product> Sorting(List<Product> products)
    {
        switch (cbox_sorting.SelectedIndex)
        {
            case 1:
                return products.OrderBy(x => x.ActualPrice).ToList();
            case 2:
                return products.OrderByDescending(x => x.ActualPrice).ToList();
        }
        return products;
    }

    private List<Product> FiltrationSearching(List<Product> products)
    {
        List<Product> filtered = [];

        if(cbox_filtration.SelectedIndex != 0)
            switch (cbox_filtration.SelectedIndex)
            {
                case 1:
                    filtered.AddRange(products.Where(x => x.ActualDiscount >= 0 && x.ActualDiscount < 10));
                    break;
                case 2:
                    filtered.AddRange(products.Where(x => x.ActualDiscount >= 10 && x.ActualDiscount < 15));
                    break;
                case 3:
                    filtered.AddRange(products.Where(x => x.ActualDiscount >= 15));
                    break;
            }
        else
            filtered.AddRange(products);

        if (tbox_searchbar.Text != "")
        {
            List<Product> searched = [];

            searched.AddRange(filtered.Where(x => x.Name.Trim().ToLower().Contains(tbox_searchbar.Text.Trim().ToLower())));
            return Sorting(searched);
        }

        return Sorting(filtered);
    }

    private void UsingSearchingAndFiltration()
    {
        _FoundedProducts.Clear();
        if(tbox_searchbar.Text != null && cbox_sorting.SelectedItem != null && cbox_filtration.SelectedItem != null)
        {
            _FoundedProducts.AddRange(FiltrationSearching(_Products));
            listbox_products.ItemsSource = _FoundedProducts.ToList();
            tblock_itemsCount.Text = $"{_FoundedProducts.Count}/{_Products.Count}";
        }
    }

    private void Panel_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (_LoggedUser.Role == 1)
        {
            Product SelectedProduct = (listbox_products.SelectedItem as Product)!;
            AddWindow addWindow = new AddWindow(SelectedProduct, _LoggedUser);
            addWindow.Show();
            Close();
        }
    }

    private void AddProduct(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_LoggedUser.Role == 1)
        {
            AddWindow addWindow = new AddWindow( _LoggedUser);
            addWindow.Show();
            Close();
        }
    }

    private void Panel_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        var ctl = sender as Control;
        var point = e.GetCurrentPoint(sender as Control);
        if (ctl != null && point.Properties.IsRightButtonPressed)
        {
            FlyoutBase.ShowAttachedFlyout(ctl);
        }
    }

    private void AddToCart(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Product SelectedProduct = (listbox_products.SelectedItem as Product)!;
        if (_NewOrder.OrderProducts.Where(x => x.Product == SelectedProduct.Id).Count() == 0)
        {
            _NewOrder.OrderProducts.Add(new OrderProduct() { Product = SelectedProduct.Id, Order = _NewOrder.Id, Quantity = 1 });
            btn_openCart.IsVisible = true;
        }
    }

    private void OpenCart(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
        CartWindow cartWindow = new CartWindow(_LoggedUser, _NewOrder);
        cartWindow.Show();
        Close();
    }
}