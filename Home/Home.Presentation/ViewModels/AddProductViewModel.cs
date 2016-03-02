
using System;
using System.Windows;
using System.Windows.Input;
using Home.Budget;
using Home.Presentation.Views;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ViewModels
{
    public class AddProductViewModel
    {

        public Window Window { get; }
        public Product Product { get; }
        public ICommand CommandOK { get; }

        public string Name
        {
            get { return Product.Name; }
            set { Product.Name = value; }
        }

        public string Brand
        {
            get { return Product.Brand; }
            set { Product.Brand = value; }
        }

        public double Volume
        {
            get { return Product.Volume; }
            set { Product.Volume = value; }
        }

        public Product.ProductVolumeType ProductVolumeType
        {
            get { return Product.Type; }
            set { Product.Type = value; }
        }

        public AddProductViewModel()
        {
            Window = new AddProductWindow { DataContext = this };
            Product = new Product();
            CommandOK = new ActionCommand((x) => Window.DialogResult = true);
        }
        
    }
}
