using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Home.Budget;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ButtonGridControl
{
    class ShowProductsGrid:ButtonGrid
    {
        private ButtonGridItem _root;

        public ICommand OpenCategoryCommand { get; }

        private const string SUBCATEGORIES = "subcategories";

        public ShowProductsGrid()
        {
            // OpenCategoryCommand = new ActionCommand(OpenCategory);
            // var root = BudgetTopic.Get().Database.AllCategory;
            // var subitems = CreateSubItems(root);
            //_root = new ButtonGridItem(root)
            // {
            //     MouseDoubleClick = OpenCategoryCommand,
            // };
            // Items = new ObservableCollection<ButtonGridItem>(subitems);

            BitmapImage bmp = new BitmapImage(new Uri(@"C:\Users\Sadako\Desktop\temp.jpg"));
            var list = new List<ButtonGridItem>()
            {
                new ButtonGridItem(new ProductCategory() {ImageName = @"C:\Users\Sadako\Desktop\temp.jpg", Name = "0 0"}),
                new ButtonGridItem(new ProductCategory() {ImageName = @"C:\Users\Sadako\Desktop\temp.jpg", Name = "0 1"}),
                new ButtonGridItem(new ProductCategory() {ImageName = @"C:\Users\Sadako\Desktop\temp.jpg", Name = "1 0"}),
                new ButtonGridItem(new ProductCategory() {ImageName = @"C:\Users\Sadako\Desktop\temp.jpg", Name = "1 1"}),
            };
            Items = new ObservableCollection<ButtonGridItem>(list);
        }

        public List<ButtonGridItem> CreateSubItems(ProductCategory category)
        {
            var result = new List<ButtonGridItem>();
            foreach (var item in category.SubCategories)
            {
                var subcategories = CreateSubItems(item);
                var btnItem = new ButtonGridItem(item) {MouseDoubleClick = OpenCategoryCommand};
                btnItem.Tag[SUBCATEGORIES] = subcategories;
            }
            result.AddRange(from item in category.Products select new ButtonGridItem(item));
            return result;

        }

        private void OpenCategory(object parameter)
        {
            var item = parameter as ButtonGridItem;
            if (item == null) return;
            var subcategories = item.Tag[SUBCATEGORIES] as List<ButtonGridItem>;
            Items = new ObservableCollection<ButtonGridItem>(subcategories);

        }
    }
}
